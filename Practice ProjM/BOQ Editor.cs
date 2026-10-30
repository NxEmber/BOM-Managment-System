using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_ProjM
{
    public partial class BOQ_Editor : Form
    {
        private const string connectionString =
            @"Data Source=NMT-DXN-LT-0200\SQLExpress;Initial Catalog=BOM;Integrated Security=True;Trust Server Certificate=True;";

        private const int MaxRemarksLength = 200;

        private static readonly Dictionary<string, Color> StatusColors = new Dictionary<string, Color>
        {
            { "V",  Color.FromArgb(198, 239, 206) },   // light green
            { "R",  Color.FromArgb(255, 199, 206) },   // light red
            { "O",  Color.FromArgb(189, 215, 238) },   // light blue
            { "PA", Color.FromArgb(255, 235, 156) }    // light amber
        };

        private bool loading = false;
        private bool locked = false;
        private string docStatus = "Draft";

        private const string KeysSql =
             @"SELECT NIAPNo, MIN(IdentificationKey) AS IdentificationKey
               FROM MPL
               WHERE ISNULL(NIAPNo, '') <> ''
               GROUP BY NIAPNo
               ORDER BY NIAPNo";

        private const string ItemsSql =
            @"SELECT SNo,
                     ISNULL(NMT_Serial_Number, ''),
                     ISNULL(Design, ''),
                     ISNULL(Description, ''),
                     CASE WHEN Type LIKE 'Custom%' THEN 1 ELSE 0 END,
                     CASE WHEN Type = 'BOP'        THEN 1 ELSE 0 END,
                     ISNULL(QTY, 0)
              FROM MPL WHERE IdentificationKey = @Key ORDER BY SNo";

        private const string SavedSql =
            @"SELECT SNo, ISNULL(Item, ''), OldQty, NewQty, ISNULL(Status, ''), ISNULL(Remarks, ''), DocStatus
              FROM BoqEdit WHERE IdentificationKey = @Key ORDER BY SNo";

        private const string DeleteEditSql = "DELETE FROM BoqEdit WHERE IdentificationKey = @Key";

        private const string InsertEditSql =
            @"INSERT INTO BoqEdit (IdentificationKey, SNo, Item, OldQty, NewQty, Status, Remarks, DocStatus)
              VALUES (@Key, @SNo, @Item, @OldQty, @NewQty, @Status, @Remarks, @DocStatus)";

        private const string SubmitSql =
            @"MERGE BoqAuthorise AS target
              USING (SELECT @Key AS IdentificationKey) AS source
                 ON target.IdentificationKey = source.IdentificationKey
              WHEN MATCHED THEN
                  UPDATE SET Status = 'Pending', SubmittedBy = @User, SubmittedAt = SYSDATETIME(),
                             AuthorisedBy = NULL, AuthorisedAt = NULL
              WHEN NOT MATCHED THEN
                  INSERT (IdentificationKey, Status, SubmittedBy) VALUES (@Key, 'Pending', @User);";

        private const string SubmitItemsSql =
            @"DELETE FROM BoqAuthoriseItem WHERE IdentificationKey = @Key;
              INSERT INTO BoqAuthoriseItem (IdentificationKey, SNo)
              SELECT IdentificationKey, SNo FROM BoqEdit WHERE IdentificationKey = @Key;";

        private const string WithdrawSql =
            @"DELETE FROM BoqAuthoriseItem WHERE IdentificationKey = @Key;
              DELETE FROM BoqAuthorise     WHERE IdentificationKey = @Key;";

        public BOQ_Editor()
        {
            InitializeComponent();
            Load += BOQ_Editor_Load;
        }
        private readonly Dictionary<string, string> keyByNiap = new Dictionary<string, string>();
        private async void BOQ_Editor_Load(object sender, EventArgs e)
        {
            SetupDataGrid();
            SetupStatusGrid();

            cb_Status.Items.Clear();
            cb_Status.Items.AddRange(new object[] { "All", "V", "R", "O", "PA", "Unmarked" });
            cb_Status.SelectedIndex = 0;

            cb_View.SelectedIndexChanged -= cb_View_SelectedIndexChanged;
            cb_View.SelectedIndexChanged += cb_View_SelectedIndexChanged;
            cb_Status.SelectedIndexChanged -= cb_Status_SelectedIndexChanged;
            cb_Status.SelectedIndexChanged += cb_Status_SelectedIndexChanged;

            guna2dgv_Data.CurrentCellDirtyStateChanged -= Data_CurrentCellDirtyStateChanged;
            guna2dgv_Data.CurrentCellDirtyStateChanged += Data_CurrentCellDirtyStateChanged;
            guna2dgv_Data.CellValueChanged -= Data_CellValueChanged;
            guna2dgv_Data.CellValueChanged += Data_CellValueChanged;

            Wire(btn_Plus, btn_Plus_Click);
            Wire(btn_Minus, btn_Minus_Click);
            Wire(btn_V, (s, args) => MarkSelected("V"));
            Wire(btn_R, (s, args) => MarkSelected("R"));
            Wire(btn_O, (s, args) => MarkSelected("O"));
            Wire(btn_PA, (s, args) => MarkSelected("PA"));
            Wire(btn_Revised, btn_Revised_Click);
            Wire(btn_Save, btn_Save_Click);
            Wire(btn_ForApproval, btn_ForApproval_Click);

            guna2dgv_Data.ColumnHeaderMouseClick += (s, e) =>
            {
                if (e.ColumnIndex != guna2dgv_Data.Columns["d_Check"].Index)
                    return;

                bool selectAll = guna2dgv_Data.Rows
                    .Cast<DataGridViewRow>()
                    .Any(r => !(r.Cells["d_Check"].Value is bool b && b));

                loading = true;

                foreach (DataGridViewRow row in guna2dgv_Data.Rows)
                    row.Cells["d_Check"].Value = selectAll;

                loading = false;

                foreach (DataGridViewRow row in guna2dgv_Data.Rows)
                    Data_CellValueChanged(guna2dgv_Data,
                        new DataGridViewCellEventArgs(
                            guna2dgv_Data.Columns["d_Check"].Index, row.Index));
            };

            await LoadKeysAsync();
            UpdateTitle();
        }

        private static void Wire(Control button, EventHandler handler)
        {
            button.Click += handler;
        }

        private void SetupDataGrid()
        {
            DataGridView grid = guna2dgv_Data;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "d_Check",
                HeaderText = "Select All",
                Width = 80
            });
            AddTextColumn(grid, "d_SNo", "S. No.", null, false);
            AddTextColumn(grid, "d_Item", "Mechanical Item", null, false);
            AddTextColumn(grid, "d_Design", "Design No.", null, false);
            AddTextColumn(grid, "d_Description", "Description", null, false);
            AddTextColumn(grid, "d_Custom", "Custom", "0;-0;", false);
            AddTextColumn(grid, "d_Bop", "BOP", "0;-0;", false);
            AddTextColumn(grid, "d_Qty", "QTY", null, true);

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewColumn column in grid.Columns)
                column.ReadOnly = column.Name != "d_Check";
        }

        private void SetupStatusGrid()
        {
            DataGridView grid = guna2dgv_Status;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();

            AddTextColumn(grid, "s_No", "S.No.", null, false);
            AddTextColumn(grid, "s_Item", "Item", null, false);
            AddTextColumn(grid, "s_Old", "Old Qty", null, false);
            AddTextColumn(grid, "s_New", "New Qty", null, false);
            AddTextColumn(grid, "s_Status", "Status", null, false);
            AddTextColumn(grid, "s_Remarks", "Remarks", null, false);
            AddTextColumn(grid, "s_MplSNo", "MPL S.No.", null, true);

            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewColumn column in grid.Columns)
                column.ReadOnly = column.Name != "s_Remarks";
        }

        private static void AddTextColumn(DataGridView grid, string name, string header, string format, bool hidden)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                Visible = !hidden
            };
            if (format != null) column.DefaultCellStyle.Format = format;
            grid.Columns.Add(column);
        }

        private async Task<List<object[]>> ReadRowsAsync(string sql, string key)
        {
            List<object[]> rows = new List<object[]>();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (key != null) cmd.Parameters.AddWithValue("@Key", key);
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

        private async Task LoadKeysAsync()
        {
            try
            {
                List<object[]> rows = await ReadRowsAsync(KeysSql, null);

                keyByNiap.Clear();
                cb_View.Items.Clear();

                foreach (object[] row in rows)
                {
                    string niap = row[0].ToString();
                    string key = row[1].ToString();

                    keyByNiap[niap] = key;
                    cb_View.Items.Add(niap);
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }
        private string GetSelectedKey()
        {
            string niap = cb_View.Text.Trim();

            return keyByNiap.TryGetValue(niap, out string key)
                ? key
                : "";
        }

        private async void cb_View_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadBomAsync();
        }

        private async Task LoadBomAsync()
        {
            string key = GetSelectedKey();
            if (key == "") return;

            loading = true;

            try
            {
                List<object[]> items = await ReadRowsAsync(ItemsSql, key);
                List<object[]> saved = await ReadRowsAsync(SavedSql, key);

                guna2dgv_Data.Rows.Clear();
                guna2dgv_Status.Rows.Clear();
                docStatus = "Draft";

                foreach (object[] item in items)
                {
                    guna2dgv_Data.Rows.Add(
                        false, item[0], item[1], item[2],
                        item[3], item[4], item[5], item[6]);
                }

                foreach (object[] edit in saved)
                {
                    DataGridViewRow topRow =
                        FindDataRow(Convert.ToInt32(edit[0]));

                    if (topRow == null) continue;

                    topRow.Cells["d_Check"].Value = true;

                    string status = edit[4].ToString();
                    if (string.IsNullOrWhiteSpace(status))
                        status = "V";

                    guna2dgv_Status.Rows.Add(
                        0, edit[1], edit[2], edit[3],
                        status, edit[5], edit[0]);

                    docStatus = edit[6].ToString();
                }

                List<object[]> approval = await ReadRowsAsync(
                    "SELECT Status FROM BoqAuthorise WHERE IdentificationKey = @Key",
                    key);

                if (approval.Count > 0)
                {
                    string approvalStatus = approval[0][0].ToString();

                    docStatus = approvalStatus == "Pending"
                        ? "For Approval"
                        : approvalStatus;
                }

                RenumberStatusGrid();
                ApplyStatusFilter();
                SetLocked(docStatus == "For Approval" || docStatus == "Released");
                UpdateTitle();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
            finally
            {
                loading = false;
            }
        }
        private DataGridViewRow FindDataRow(int mplSerial)
        {
            foreach (DataGridViewRow row in guna2dgv_Data.Rows)
                if (Convert.ToInt32(row.Cells["d_SNo"].Value) == mplSerial) return row;
            return null;
        }

        private DataGridViewRow FindStatusRow(int mplSerial)
        {
            foreach (DataGridViewRow row in guna2dgv_Status.Rows)
                if (Convert.ToInt32(row.Cells["s_MplSNo"].Value) == mplSerial) return row;
            return null;
        }

        private void Data_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (guna2dgv_Data.IsCurrentCellDirty)
                guna2dgv_Data.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void Data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (loading || e.RowIndex < 0) return;
            if (guna2dgv_Data.Columns[e.ColumnIndex].Name != "d_Check") return;

            DataGridViewRow topRow = guna2dgv_Data.Rows[e.RowIndex];
            bool ticked = topRow.Cells["d_Check"].Value is bool value && value;
            int mplSerial = Convert.ToInt32(topRow.Cells["d_SNo"].Value);

            DataGridViewRow existing = FindStatusRow(mplSerial);
            if (ticked && existing == null)
            {
                object quantity = topRow.Cells["d_Qty"].Value;
                guna2dgv_Status.Rows.Add(0, topRow.Cells["d_Item"].Value, quantity, quantity, "V", "", mplSerial);
            }
            else if (!ticked && existing != null)
            {
                guna2dgv_Status.Rows.Remove(existing);
            }

            RenumberStatusGrid();
            ApplyStatusFilter();
        }

        private void RenumberStatusGrid()
        {
            for (int i = 0; i < guna2dgv_Status.Rows.Count; i++)
                guna2dgv_Status.Rows[i].Cells["s_No"].Value = i + 1;
        }

        private void cb_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyStatusFilter();
        }

        private void ApplyStatusFilter()
        {
            string filter = cb_Status.Text.Trim();
            guna2dgv_Status.CurrentCell = null;
            foreach (DataGridViewRow row in guna2dgv_Status.Rows)
            {
                string status = row.Cells["s_Status"].Value?.ToString() ?? "";
                bool show = filter == "" || filter == "All"
                            || (filter == "Unmarked" ? status == "" : status == filter);
                row.Visible = show;
            }
            RefreshColors();
        }

        private void RefreshColors()
        {
            foreach (DataGridViewRow row in guna2dgv_Status.Rows)
            {
                string status = row.Cells["s_Status"].Value?.ToString() ?? "";
                ColorCell(row.Cells["s_Status"], status);
            }
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

        private void btn_Plus_Click(object sender, EventArgs e) => ChangeNewQty(+1);

        private void btn_Minus_Click(object sender, EventArgs e) => ChangeNewQty(-1);

        private void ChangeNewQty(int change)
        {
            if (!HasSelectedStatusRows()) return;
            foreach (DataGridViewRow row in guna2dgv_Status.SelectedRows)
            {
                int.TryParse(row.Cells["s_New"].Value?.ToString(), out int quantity);
                row.Cells["s_New"].Value = Math.Max(0, quantity + change);
            }
        }

        private void MarkSelected(string code)
        {
            if (!HasSelectedStatusRows()) return;
            foreach (DataGridViewRow row in guna2dgv_Status.SelectedRows)
                row.Cells["s_Status"].Value = code;
            ApplyStatusFilter();
        }

        private bool HasSelectedStatusRows()
        {
            if (locked) return false;
            if (guna2dgv_Status.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select one or more rows in the lower table first.");
                return false;
            }
            return true;
        }

        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (await SaveAsync(null))
                MessageBox.Show("Saved.");
        }

        private async void btn_ForApproval_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show(
                "Send this for approval? It cannot be edited until it is marked Revised.",
                "For Approval", MessageBoxButtons.YesNo);
            if (answer != DialogResult.Yes) return;

            if (await SaveAsync("For Approval"))
            {
                SetLocked(true);
                MessageBox.Show("Sent for approval.");
            }
        }

        private async void btn_Revised_Click(object sender, EventArgs e)
        {
            if (await SaveAsync("Revised"))
            {
                SetLocked(false);
                MessageBox.Show("Marked as Revised. You can edit again.");
            }
        }

        private async Task<bool> SaveAsync(string newDocStatus)
        {
            string key = GetSelectedKey();
            if (key == "")
            {
                MessageBox.Show("Select a View first.");
                return false;
            }
            if (guna2dgv_Status.Rows.Count == 0)
            {
                MessageBox.Show("Tick at least one item in the upper table first.");
                return false;
            }

            string statusToSave = newDocStatus ?? docStatus;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();
                    using (SqlTransaction tx = con.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand delete = new SqlCommand(DeleteEditSql, con, tx))
                            {
                                delete.Parameters.AddWithValue("@Key", key);
                                await delete.ExecuteNonQueryAsync();
                            }

                            foreach (DataGridViewRow row in guna2dgv_Status.Rows)
                            {
                                string remarks = row.Cells["s_Remarks"].Value?.ToString() ?? "";
                                if (remarks.Length > MaxRemarksLength) remarks = remarks.Substring(0, MaxRemarksLength);

                                using (SqlCommand insert = new SqlCommand(InsertEditSql, con, tx))
                                {
                                    insert.Parameters.AddWithValue("@Key", key);
                                    insert.Parameters.AddWithValue("@SNo", Convert.ToInt32(row.Cells["s_MplSNo"].Value));
                                    insert.Parameters.AddWithValue("@Item", row.Cells["s_Item"].Value?.ToString() ?? "");
                                    insert.Parameters.AddWithValue("@OldQty", Convert.ToInt32(row.Cells["s_Old"].Value));
                                    insert.Parameters.AddWithValue("@NewQty", Convert.ToInt32(row.Cells["s_New"].Value));
                                    insert.Parameters.AddWithValue("@Status", row.Cells["s_Status"].Value?.ToString() ?? "");
                                    insert.Parameters.AddWithValue("@Remarks", remarks);
                                    insert.Parameters.AddWithValue("@DocStatus", statusToSave);
                                    await insert.ExecuteNonQueryAsync();
                                }
                            }
                            if (statusToSave == "For Approval")
                            {
                                using (SqlCommand submit = new SqlCommand(SubmitSql, con, tx))
                                {
                                    submit.Parameters.AddWithValue("@Key", key);
                                    submit.Parameters.AddWithValue("@User", Environment.UserName); // Change this to the actual user When finalised
                                    await submit.ExecuteNonQueryAsync();
                                }
                                using (SqlCommand submitItems = new SqlCommand(SubmitItemsSql, con, tx))
                                {
                                    submitItems.Parameters.AddWithValue("@Key", key);
                                    await submitItems.ExecuteNonQueryAsync();
                                }
                            }
                            else if (statusToSave == "Revised")
                            {
                                using (SqlCommand withdraw = new SqlCommand(WithdrawSql, con, tx))
                                {
                                    withdraw.Parameters.AddWithValue("@Key", key);
                                    await withdraw.ExecuteNonQueryAsync();
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
                docStatus = statusToSave;
                UpdateTitle();
                return true;
            }
            catch (Exception ex)
            {
                ShowError(ex);
                return false;
            }
        }

        private void SetLocked(bool isLocked)
        {
            locked = isLocked;
            guna2dgv_Data.ReadOnly = isLocked;
            guna2dgv_Status.ReadOnly = isLocked;
            if (!isLocked)
            {
                foreach (DataGridViewColumn column in guna2dgv_Data.Columns)
                    column.ReadOnly = column.Name != "d_Check";
                foreach (DataGridViewColumn column in guna2dgv_Status.Columns)
                    column.ReadOnly = column.Name != "s_Remarks";
            }
            foreach (Control button in new Control[] { btn_Plus, btn_Minus, btn_V, btn_R, btn_O, btn_PA, btn_Save, btn_ForApproval })
                button.Enabled = !isLocked;
            UpdateTitle();
        }

        private void UpdateTitle()
        {
            string niap = cb_View.Text.Trim();
            Text = niap == "" ? "BOQ_Editor" : "BOQ_Editor - " + niap + " [" + docStatus + "]";
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {

        }
    }
}