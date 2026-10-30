using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_ProjM
{
    public partial class BOQ_Authorise : Form
    {
        private const string connectionString =
            @"Data Source=NMT-DXN-LT-0200\SQLExpress;Initial Catalog=BOM;Integrated Security=True;Trust Server Certificate=True;";

        private static readonly Dictionary<string, Color> StatusColors = new Dictionary<string, Color>
        {
            { "Pending",  Color.FromArgb(255, 235, 156) },   // light amber
            { "Released", Color.FromArgb(198, 239, 206) }    // light green
        };

        private bool loading = false;
        private bool busy = false;
        private int itemsRequest = 0;    

        private const string BoqSql =
            @"SELECT ROW_NUMBER() OVER (ORDER BY a.SubmittedAt DESC),
                     '',
                     ISNULL(MAX(m.Machine), ''),
                     ISNULL(MAX(m.Design), ''),
                     MAX(i.Status),
                     a.IdentificationKey,
                     COUNT(*),
                     ISNULL(a.SubmittedBy, ''),
                     a.SubmittedAt,
                     SUM(CASE WHEN m.Type LIKE 'Custom%' THEN 1 ELSE 0 END),
                     SUM(CASE WHEN m.Type = 'BOP'        THEN 1 ELSE 0 END)
              FROM BoqAuthorise a
              JOIN BoqAuthoriseItem i ON i.IdentificationKey = a.IdentificationKey
              JOIN MPL m ON m.IdentificationKey = i.IdentificationKey AND m.SNo = i.SNo
              WHERE i.Status = @Status
              GROUP BY a.IdentificationKey, a.SubmittedBy, a.SubmittedAt
              HAVING @Type = 'All'
                  OR (@Type = 'BOP'    AND SUM(CASE WHEN m.Type = 'BOP'        THEN 1 ELSE 0 END) > 0)
                  OR (@Type = 'Custom' AND SUM(CASE WHEN m.Type LIKE 'Custom%' THEN 1 ELSE 0 END) > 0)
              ORDER BY a.SubmittedAt DESC";

        private const string ItemsSqlStart =
            @"SELECT i.IdentificationKey, ISNULL(m.Description, ''), e.NewQty, i.Status, i.SNo
              FROM BoqAuthoriseItem i
              JOIN BoqEdit e ON e.IdentificationKey = i.IdentificationKey AND e.SNo = i.SNo
              JOIN MPL m ON m.IdentificationKey = i.IdentificationKey AND m.SNo = i.SNo
              WHERE i.Status = @Status
                AND i.IdentificationKey IN (";

        private const string ItemsSqlEnd =
            @")
                AND (@Type = 'All'
                     OR (@Type = 'BOP'    AND m.Type = 'BOP')
                     OR (@Type = 'Custom' AND m.Type LIKE 'Custom%'))
              ORDER BY i.IdentificationKey, i.SNo";

        private const string UpdateBoqStatusSql =
            @"UPDATE BoqAuthorise
              SET Status = CASE
                      WHEN EXISTS (SELECT 1 FROM BoqAuthoriseItem i
                                   WHERE i.IdentificationKey = BoqAuthorise.IdentificationKey AND i.Status = 'Pending')  THEN 'Pending'
                      WHEN EXISTS (SELECT 1 FROM BoqAuthoriseItem i
                                   WHERE i.IdentificationKey = BoqAuthorise.IdentificationKey AND i.Status = 'Rejected') THEN 'Rejected'
                      ELSE 'Released' END,
                  AuthorisedBy = @User,
                  AuthorisedAt = SYSDATETIME()
              WHERE IdentificationKey = @Key";

        public BOQ_Authorise()
        {
            InitializeComponent();
            Load += BOQ_Authorise_Load;

            // In the constructor after InitializeComponent(), or in Form_Load:
            guna2dgv_Items.SelectionMode = DataGridViewSelectionMode.CellSelect;
            guna2dgv_Items.ColumnHeaderMouseClick += guna2dgv_Items_ColumnHeaderMouseClick;
        }

        private async void BOQ_Authorise_Load(object sender, EventArgs e)
        {
            SetupBoqGrid();
            SetupItemsGrid();

            cb_Type.Items.Clear();
            cb_Type.Items.AddRange(new object[] { "All", "BOP", "Custom" });
            cb_Type.SelectedIndex = 0;
            rb_Pending.Checked = true;

            cb_Type.SelectedIndexChanged -= Filter_Changed;
            cb_Type.SelectedIndexChanged += Filter_Changed;
            rb_Pending.CheckedChanged -= Filter_Changed;
            rb_Pending.CheckedChanged += Filter_Changed;
            rb_Released.CheckedChanged -= Filter_Changed;
            rb_Released.CheckedChanged += Filter_Changed;

            guna2dgv_BOQ.CurrentCellDirtyStateChanged -= Boq_CurrentCellDirtyStateChanged;
            guna2dgv_BOQ.CurrentCellDirtyStateChanged += Boq_CurrentCellDirtyStateChanged;
            guna2dgv_BOQ.CellValueChanged -= Boq_CellValueChanged;
            guna2dgv_BOQ.CellValueChanged += Boq_CellValueChanged;

            btn_Approve.Click -= btn_Approve_Click;
            btn_Approve.Click += btn_Approve_Click;
            btn_Reject.Click -= btn_Reject_Click;
            btn_Reject.Click += btn_Reject_Click;

            await LoadBoqsAsync();
        }

        private void SetupBoqGrid()
        {
            DataGridView grid = guna2dgv_BOQ;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();

            grid.Columns.Add(new DataGridViewCheckBoxColumn { Name = "a_Check", HeaderText = "", Width = 60 });
            AddTextColumn(grid, "a_SNo", "SNo", null);
            AddTextColumn(grid, "a_WO", "WO", null);
            AddTextColumn(grid, "a_Machine", "M/C Name", null);
            AddTextColumn(grid, "a_Design", "Design No.", null);
            AddTextColumn(grid, "a_Status", "Status", null);
            AddTextColumn(grid, "a_Bom", "Identifier", null);
            AddTextColumn(grid, "a_Boq", "BOQ", null);
            AddTextColumn(grid, "a_UploadBy", "Upload By", null);
            AddTextColumn(grid, "a_Date", "Date-Time", "dd-MMM-yyyy HH:mm");
            AddTextColumn(grid, "a_Custom", "Custom", "0;-0;");     
            AddTextColumn(grid, "a_Bop", "BOP", "0;-0;");

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewColumn column in grid.Columns)
                column.ReadOnly = column.Name != "a_Check";         
        }

        private void SetupItemsGrid()
        {
            DataGridView grid = guna2dgv_Items;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();

            AddTextColumn(grid, "i_SNo", "S.No", null);
            AddTextColumn(grid, "i_Niap", "Identifier", null);
            AddTextColumn(grid, "i_Description", "Description", null);
            AddTextColumn(grid, "i_Qty", "QTY", null);
            AddTextColumn(grid, "i_Status", "Status", null);
            AddTextColumn(grid, "i_MplSNo", "MPL S.No.", null);
            grid.Columns["i_MplSNo"].Visible = false;               

            grid.ReadOnly = true;                                   
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private static void AddTextColumn(DataGridView grid, string name, string header, string format)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            if (format != null) column.DefaultCellStyle.Format = format;
            grid.Columns.Add(column);
        }

        private string SelectedDocStatus() => rb_Released.Checked ? "Released" : "Pending";

        private async void Filter_Changed(object sender, EventArgs e)
        {
            if (sender is RadioButton radio && !radio.Checked) return;
            await LoadBoqsAsync();
        }

        private async Task<List<object[]>> ReadRowsAsync(string sql, Action<SqlCommand> addParameters)
        {
            List<object[]> rows = new List<object[]>();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                addParameters(cmd);
                await con.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        object[] values = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                            values[i] = reader.IsDBNull(i) ? "" : reader[i];
                        rows.Add(values);
                    }
                }
            }
            return rows;
        }

        private async Task LoadBoqsAsync(List<string> keepTicked = null)
        {
            loading = true;
            bool anyTicked = false;
            try
            {
                string docStatus = SelectedDocStatus();
                string type = cb_Type.Text.Trim() == "" ? "All" : cb_Type.Text.Trim();

                List<object[]> boqs = await ReadRowsAsync(BoqSql, cmd =>
                {
                    cmd.Parameters.AddWithValue("@Status", docStatus);
                    cmd.Parameters.AddWithValue("@Type", type);
                });

                guna2dgv_BOQ.Rows.Clear();
                guna2dgv_Items.Rows.Clear();
                foreach (object[] r in boqs)
                {
                    bool ticked = keepTicked != null && keepTicked.Contains(r[5].ToString());
                    anyTicked = anyTicked || ticked;
                    guna2dgv_BOQ.Rows.Add(ticked, r[0], r[1], r[2], r[3], r[4], r[5], r[6], r[7], r[8], r[9], r[10]);
                }
                guna2dgv_BOQ.ClearSelection();

                btn_Approve.Enabled = rb_Pending.Checked;
                btn_Reject.Enabled = rb_Pending.Checked;
            }
            catch (Exception ex) { ShowError(ex); }
            finally { loading = false; }

            if (anyTicked) await LoadItemsAsync();
        }

        private void Boq_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (guna2dgv_BOQ.IsCurrentCellDirty)
                guna2dgv_BOQ.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private async void Boq_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (loading || e.RowIndex < 0) return;
            if (guna2dgv_BOQ.Columns[e.ColumnIndex].Name != "a_Check") return;
            await LoadItemsAsync();
        }

        private List<string> TickedKeys()
        {
            List<string> keys = new List<string>();
            foreach (DataGridViewRow row in guna2dgv_BOQ.Rows)
                if (row.Cells["a_Check"].Value is bool ticked && ticked)
                    keys.Add(row.Cells["a_Bom"].Value?.ToString() ?? "");
            return keys.Where(key => key != "").ToList();
        }

        private async Task LoadItemsAsync()
        {
            int request = ++itemsRequest;
            List<string> keys = TickedKeys();
            if (keys.Count == 0)
            {
                guna2dgv_Items.Rows.Clear();
                return;
            }

            try
            {
                string type = cb_Type.Text.Trim() == "" ? "All" : cb_Type.Text.Trim();
                string sql = ItemsSqlStart +
                             string.Join(",", keys.Select((key, i) => "@k" + i)) +
                             ItemsSqlEnd;

                List<object[]> items = await ReadRowsAsync(sql, cmd =>
                {
                    for (int i = 0; i < keys.Count; i++)
                        cmd.Parameters.AddWithValue("@k" + i, keys[i]);
                    cmd.Parameters.AddWithValue("@Type", type);
                    cmd.Parameters.AddWithValue("@Status", SelectedDocStatus());
                });

                if (request != itemsRequest) return;     

                guna2dgv_Items.Rows.Clear();
                int number = 1;
                foreach (object[] r in items)
                {
                    guna2dgv_Items.Rows.Add(number++, r[0], r[1], r[2], r[3], r[4]);
                }

                foreach (DataGridViewRow row in guna2dgv_Items.Rows)
                    ColorCell(row.Cells["i_Status"], row.Cells["i_Status"].Value?.ToString() ?? "");
                guna2dgv_Items.ClearSelection();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private static void ColorCell(DataGridViewCell cell, string status)
        {
            if (StatusColors.TryGetValue(status, out Color color))
            {
                cell.Style.BackColor = color;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor =
                    Color.FromArgb(Math.Max(0, color.R - 50), Math.Max(0, color.G - 50), Math.Max(0, color.B - 50));
                cell.Style.SelectionForeColor = Color.Black;
            }
            else
            {
                cell.Style.BackColor = Color.Empty;
                cell.Style.ForeColor = Color.Empty;
                cell.Style.SelectionBackColor = Color.Empty;
                cell.Style.SelectionForeColor = Color.Empty;
            }
        }

        private async void btn_Approve_Click(object sender, EventArgs e)
        {
            await ChangeItemStatusAsync("Released", "Approve");
        }

        private async void btn_Reject_Click(object sender, EventArgs e)
        {
            await ChangeItemStatusAsync("Rejected", "Reject");
        }

        private async Task ChangeItemStatusAsync(string newStatus, string actionName)
        {
            if (busy) return;

            List<KeyValuePair<string, int>> picked = new List<KeyValuePair<string, int>>();
            foreach (DataGridViewRow row in guna2dgv_Items.SelectedRows)
            {
                string key = row.Cells["i_Niap"].Value?.ToString() ?? "";
                if (key != "" && int.TryParse(row.Cells["i_MplSNo"].Value?.ToString(), out int serial))
                    picked.Add(new KeyValuePair<string, int>(key, serial));
            }

            if (picked.Count == 0)
            {
                MessageBox.Show("Select one or more items in the lower table first.");
                return;
            }

            DialogResult answer = MessageBox.Show(
                actionName + " " + picked.Count + " item(s)?", actionName, MessageBoxButtons.YesNo);
            if (answer != DialogResult.Yes) return;

            busy = true;
            try
            {
                List<string> stillTicked = TickedKeys();
                int changed = 0;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    using (SqlTransaction tx = con.BeginTransaction())
                    {
                        try
                        {
                            foreach (KeyValuePair<string, int> item in picked)
                            {
                                using (SqlCommand cmd = new SqlCommand(
                                    @"UPDATE BoqAuthoriseItem
                                      SET Status = @New, AuthorisedBy = @User, AuthorisedAt = SYSDATETIME()
                                      WHERE IdentificationKey = @Key AND SNo = @SNo AND Status = 'Pending'", con, tx))
                                {
                                    cmd.Parameters.AddWithValue("@New", newStatus);
                                    cmd.Parameters.AddWithValue("@User", Environment.UserName); // Change this to the actual user when finalised
                                    cmd.Parameters.AddWithValue("@Key", item.Key);
                                    cmd.Parameters.AddWithValue("@SNo", item.Value);
                                    changed += await cmd.ExecuteNonQueryAsync();
                                }
                            }

                            foreach (string key in picked.Select(item => item.Key).Distinct())
                            {
                                using (SqlCommand cmd = new SqlCommand(UpdateBoqStatusSql, con, tx))
                                {
                                    cmd.Parameters.AddWithValue("@User", Environment.UserName);
                                    cmd.Parameters.AddWithValue("@Key", key);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                            tx.Commit();
                        }
                        catch
                        {
                            tx.Rollback();
                            throw;
                        }
                    }
                }

                await LoadBoqsAsync(stillTicked);
                MessageBox.Show((newStatus == "Released" ? "Approved " : "Rejected ") + changed + " item(s).");
            }
            catch (Exception ex) { ShowError(ex); }
            finally { busy = false; }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool headerChecked = false;

        private void guna2dgv_Items_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            guna2dgv_Items.EndEdit();
            guna2dgv_Items.ClearSelection();

            bool isCheckColumn = guna2dgv_Items.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn;
            if (isCheckColumn)
                headerChecked = !headerChecked;

            foreach (DataGridViewRow row in guna2dgv_Items.Rows)
            {
                if (row.IsNewRow) continue;

                row.Cells[e.ColumnIndex].Selected = true;

                if (isCheckColumn)
                    row.Cells[e.ColumnIndex].Value = headerChecked;
            }

            guna2dgv_Items.RefreshEdit();
        }
    }
}