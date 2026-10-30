using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_ProjM
{
    public partial class Assignment : Form
    {
        private const string connectionString =
            @"Data Source=NMT-DXN-LT-0200\SQLExpress;Initial Catalog=BOM;Integrated Security=True;Trust Server Certificate=True;";

        private bool loading;
        private bool saving;

        private const string UniteSql = @"
    MERGE dbo.UnifiedAssignment AS target
    USING
    (
        SELECT
            MAX(ISNULL(m.NIAPNo, '')) AS NiapNo,
            MAX(ISNULL(m.IdentificationKey, '')) AS Identifier,
            MAX(ISNULL(m.Design, '')) AS Design,
            CASE
                WHEN MAX(CASE WHEN i.Status = 'Released'
                              THEN 1 ELSE 0 END) = 1
                    THEN 'Released'
                WHEN MAX(CASE WHEN i.Status = 'Partly released'
                              THEN 1 ELSE 0 END) = 1
                    THEN 'Partly released'
                ELSE ISNULL(MAX(a.Status), 'Not sent')
            END AS Status
        FROM dbo.MPL m
        LEFT JOIN dbo.BoqAuthorise a
            ON a.IdentificationKey = m.IdentificationKey
        LEFT JOIN dbo.BoqAuthoriseItem i
            ON i.IdentificationKey = m.IdentificationKey
           AND i.SNo = m.SNo
        WHERE m.IdentificationKey = @Key
        GROUP BY m.IdentificationKey
    ) AS source
    ON target.NiapNo = source.NiapNo

    WHEN MATCHED THEN
        UPDATE SET
            target.Identifier = source.Identifier,
            target.Design = source.Design,
            target.Status = source.Status

    WHEN NOT MATCHED BY TARGET THEN
        INSERT (NiapNo, Identifier, Design, Status)
        VALUES (source.NiapNo, source.Identifier,
                source.Design, source.Status);";


        private const string ItemsSql = @"
                                        SELECT
                                           m.IdentificationKey AS IdentificationKey,
                                           MAX(ISNULL(m.IdentificationKey, '')) AS Identifier,
                                           MAX(ISNULL(m.NIAPNo, '')) AS NiapNo,
                                           MAX(ISNULL(m.Design, '')) AS Design,
                                        CASE
                                           WHEN MAX(CASE WHEN i.Status = 'Released'
                                                THEN 1 ELSE 0 END) = 0
                                                THEN ISNULL(MAX(ba.Status), 'Not sent')
                                           WHEN MAX(CASE WHEN i.Status = 'Released'
                                                THEN 1 ELSE 0 END) = 1
                                            AND MAX(CASE WHEN ba.Status = 'Released'
                                                THEN 1 ELSE 0 END) = 1
                                                THEN 'Released'
                                           ELSE 'Partly released'
                                        END AS Status,
                                        CAST(
                                            CASE WHEN EXISTS (
                                                              SELECT 1
                                                              FROM dbo.AssignedItem ai
                                                              WHERE ai.IdentificationKey = m.IdentificationKey
                                                             )
                                                       THEN 1 ELSE 0 END
                                                       AS bit) AS Assigned
                                        FROM dbo.MPL m
                                        LEFT JOIN dbo.BoqAuthorise ba
                                             ON ba.IdentificationKey = m.IdentificationKey
                                        LEFT JOIN dbo.BoqAuthoriseItem i
                                             ON i.IdentificationKey = m.IdentificationKey
                                             AND i.SNo = m.SNo
                                        GROUP BY m.IdentificationKey
                                        ORDER BY m.IdentificationKey;";

        private const string GetMplItemsSql = @"
                                              SELECT m.SNo, ISNULL(m.Design, ''), ISNULL(m.Type, ''),
                                                            ISNULL(e.NewQty, ISNULL(m.QTY, 0))
                                              FROM MPL m
                                              LEFT JOIN BoqEdit e
                                                   ON e.IdentificationKey = m.IdentificationKey AND e.SNo = m.SNo
                                              WHERE m.IdentificationKey = @Key
                                              ORDER BY m.SNo;";

        private const string SaveAssignedSql = @"
                                              MERGE dbo.AssignedItem AS target
                                              USING (
                                                    SELECT @Key AS IdentificationKey
                                                    ) AS source
                                              ON target.IdentificationKey = source.IdentificationKey
                                              AND target.SNo = 0
 
                                              WHEN MATCHED THEN
                                              UPDATE SET
                                                  Design = @Design,
                                                  NIAPNo = @NiapNo,
                                                  AssignedBy = @User,
                                                  AssignedAt = SYSDATETIME()

                                              WHEN NOT MATCHED THEN
                                                  INSERT (IdentificationKey, SNo, Design, NIAPNo, AssignedBy)
                                                  VALUES (@Key, 0, @Design, @NiapNo, @User);"; 

        public List<AssignedItem> AssignedItems { get; private set; } = new List<AssignedItem>();

        public Assignment()
        {
            InitializeComponent();
            Load += Assignment_Load;
        }

        private async void Assignment_Load(object sender, EventArgs e)
        {
            SetupItemsGrid();

            btn_Assigned.Click -= btn_AssignedSelect_Click;
            btn_Assigned.Click += btn_AssignedSelect_Click;
            btn_Clear.Click -= btn_Clear_Click;
            btn_Clear.Click += btn_Clear_Click;

            await LoadItemsAsync();
        }

        private void SetupItemsGrid()
        {
            var grid = guna2dgv_Items;
            grid.Columns.Clear();
            grid.AutoGenerateColumns = false;

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "col_IdentificationKey",
                Visible = false
            });

            AddColumn(grid, "col_Identifier", "Identifier");
            AddColumn(grid, "col_NiapNo", "NIAP No.");
            AddColumn(grid, "col_DesignNo", "Design");
            AddColumn(grid, "col_Status", "Status");

            grid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "col_Assigned",
                HeaderText = "Assigned",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grid.CellFormatting -= guna2dgv_Items_CellFormatting;
            grid.CellFormatting += guna2dgv_Items_CellFormatting;
        }
        private static void AddColumn(DataGridView grid, string name, string header)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
        }

        private void guna2dgv_Items_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridView grid = (DataGridView)sender;
            string columnName = grid.Columns[e.ColumnIndex].Name;
            string value = e.Value?.ToString() ?? "";

            if (columnName == "col_Status")
            {
                if (value.Equals("Released", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.SelectionBackColor = Color.Green;
                    e.CellStyle.SelectionForeColor = Color.White;
                }
                else
                {
                    e.CellStyle.BackColor = grid.DefaultCellStyle.BackColor;
                    e.CellStyle.ForeColor = grid.DefaultCellStyle.ForeColor;
                }
            }
        }

        private async Task LoadItemsAsync()
        {
            if (loading) return;
            loading = true;
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(ItemsSql, con))
                {
                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        table.Load(reader);
                }

               guna2dgv_Items.Rows.Clear();

                foreach (DataRow row in table.Rows)
                {
                    guna2dgv_Items.Rows.Add(
                        row["IdentificationKey"],
                        row["Identifier"],
                        row["NiapNo"],
                        row["Design"],
                        row["Status"],
                        Convert.ToBoolean(row["Assigned"])
                    );
                }

                guna2dgv_Items.ClearSelection();
                AssignedItems = new List<AssignedItem>();

                guna2dgv_Items.ClearSelection();
                guna2dgv_Items.ClearSelection();
                AssignedItems = new List<AssignedItem>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                loading = false;
            }
        }

        private async void btn_AssignedSelect_Click(object sender, EventArgs e)
        {
            if (saving) return;

            var rows = guna2dgv_Items.SelectedRows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow)
                .ToList();

            if (rows.Count == 0)
            {
                MessageBox.Show("Select one or more rows first.");
                return;
            }

            saving = true;
            btn_Assigned.Enabled = false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    foreach (var row in rows)
                    {
                        string key = Convert.ToString(row.Cells["col_IdentificationKey"].Value) ?? "";

                        string design = Convert.ToString(
                            row.Cells["col_DesignNo"].Value) ?? "";

                        string niapNo = Convert.ToString(
                            row.Cells["col_NiapNo"].Value) ?? "";

                        if (string.IsNullOrWhiteSpace(key))
                            continue;

                        using (SqlCommand cmd = new SqlCommand(SaveAssignedSql, con))
                        {
                            cmd.Parameters.Add("@Key", SqlDbType.NVarChar, 255).Value = key;
                            cmd.Parameters.Add("@Design", SqlDbType.NVarChar, 255).Value = design;
                            cmd.Parameters.Add("@User", SqlDbType.NVarChar, 256).Value =
                                Environment.UserName;
                            cmd.Parameters.Add("@NiapNo", SqlDbType.NVarChar, 255).Value = niapNo;

                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }

                await LoadItemsAsync();

                MessageBox.Show("Selected rows marked as assigned.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Assignment Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                saving = false;
                btn_Assigned.Enabled = true;
            }
        }

        private async Task SaveSelectedNiapsAsync(List<string> keys)
        {
            if (keys == null || keys.Count == 0)
            {
                MessageBox.Show("Please select at least one NIAP.");
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();

                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        foreach (string key in keys.Distinct())
                        {
                            List<AssignedItem> items = new List<AssignedItem>();

                            // Fetch items for the selected key
                            using (SqlCommand cmd = new SqlCommand(GetMplItemsSql, con, tx))
                            {
                                cmd.Parameters.Add("@Key", SqlDbType.NVarChar, 255).Value = key;

                                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                                {
                                    while (await reader.ReadAsync())
                                    {
                                        items.Add(new AssignedItem
                                        {
                                            NiapNo = key,
                                            MplSNo = Convert.ToInt32(reader[0]),
                                            DesignNo = Convert.ToString(reader[1]) ?? "",
                                            ItemType = Convert.ToString(reader[2]) ?? "",
                                            Quantity = Convert.ToInt32(reader[3]),
                                            Unite = "Nos",
                                            Assigned = true

                                        });
                                    }
                                }
                            }

                            // Save the fetched items
                            foreach (AssignedItem item in items)
                            {
                                using (SqlCommand cmd = new SqlCommand(SaveAssignedSql, con, tx))
                                {
                                    cmd.Parameters.Add("@Key", SqlDbType.NVarChar, 255).Value = item.NiapNo;
                                    cmd.Parameters.Add("@SNo", SqlDbType.Int).Value = item.MplSNo;
                                    cmd.Parameters.Add("@Design", SqlDbType.NVarChar, 255).Value = item.DesignNo;
                                    cmd.Parameters.Add("@Type", SqlDbType.NVarChar, 100).Value = item.ItemType;
                                    cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = item.Quantity;
                                    cmd.Parameters.Add("@Units", SqlDbType.NVarChar, 50).Value = item.Unite;
                                    cmd.Parameters.Add("@User", SqlDbType.NVarChar, 256).Value = Environment.UserName;

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }

                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        try { tx.Rollback(); } catch { }

                        MessageBox.Show(
                            $"Assignment failed:\n{ex.Message}",
                            "Save Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        throw;
                    }
                }
            }

            // Refresh the grid after a successful save
            await LoadItemsAsync();
        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            guna2dgv_Items.ClearSelection();
            guna2dgv_Items.CurrentCell = null;
            AssignedItems.Clear();
        }

        private async void btn_Unite_Click(object sender, EventArgs e)
        {
            // Collect NIAP numbers already marked as Assigned.
            List<string> keys = guna2dgv_Items.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow &&
                            Convert.ToBoolean(r.Cells["col_Assigned"].Value ?? false))
               .Select(r => Convert.ToString(r.Cells["col_IdentificationKey"].Value) ?? "")
                .Where(k => !string.IsNullOrWhiteSpace(k))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (keys.Count == 0)
            {
                MessageBox.Show(
                    "No assigned items are available to transfer.",
                    "Nothing to Unite",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            btn_Unite.Enabled = false;

            try
            {
                int transferred = 0;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    using (SqlTransaction tx = con.BeginTransaction())
                    {
                        try
                        {
                            foreach (string key in keys)
                            {
                                using (SqlCommand cmd = new SqlCommand(
                                    UniteSql, con, tx))
                                {
                                    cmd.Parameters.Add(
                                        "@Key", SqlDbType.NVarChar, 255).Value = key;

                                    transferred += await cmd.ExecuteNonQueryAsync();
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

                MessageBox.Show(
                    "Assignment data transferred successfully.\n" +
                    "NIAP numbers processed: " + keys.Count +
                    "\nDatabase rows inserted or updated: " + transferred,
                    "Unite Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unite Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btn_Unite.Enabled = true;
            }
        
        }
    }

    public class AssignedItem
    {
        public int SerialNumber { get; set; }
        public int MplSNo { get; set; }
        public string NiapNo { get; set; } = "";
        public string DesignNo { get; set; } = "";
        public string ItemType { get; set; } = "";
        public int Quantity { get; set; }
        public string Unite { get; set; } = "Nos";
        public bool Assigned { get; set; }
    }
}
