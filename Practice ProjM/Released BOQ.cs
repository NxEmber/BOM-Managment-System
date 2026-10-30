using ClosedXML.Excel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_ProjM
{
    public partial class Released_BOQ : Form
    {
        private const string connectionString =
            @"Data Source=NMT-DXN-LT-0200\SQLExpress;Initial Catalog=BOM;Integrated Security=True;Trust Server Certificate=True;";

        private bool loading = false;         
        private bool busy = false;            
        private int itemsRequest = 0;         
        private string currentDataKey = "";   

        private const string BoqSql =
            @"SELECT ROW_NUMBER() OVER (ORDER BY m.IdentificationKey),
                     m.IdentificationKey,
                     ISNULL(MAX(m.Design), ''),
                     ISNULL(MAX(m.Machine), ''),
                     CASE
                         WHEN a.IdentificationKey IS NULL THEN 'Not sent'
                         WHEN SUM(CASE WHEN i.Status = 'Released' THEN 1 ELSE 0 END) = 0 THEN a.Status
                         WHEN a.Status = 'Released' THEN 'Released'
                         ELSE 'Partly released'
                     END
              FROM MPL m
              LEFT JOIN BoqAuthorise     a ON a.IdentificationKey = m.IdentificationKey
              LEFT JOIN BoqAuthoriseItem i ON i.IdentificationKey = m.IdentificationKey AND i.SNo = m.SNo
              GROUP BY m.IdentificationKey, a.IdentificationKey, a.Status
              ORDER BY m.IdentificationKey";

        private const string ItemsSql =
            @"SELECT ISNULL(m.NMT_Serial_Number, ''),
                     ISNULL(m.PN, ''),
                     ISNULL(m.Description, ''),
                     ISNULL(m.Type, ''),
                     ISNULL(e.NewQty, ISNULL(m.QTY, 0)),
                     ISNULL(i.Status, 'Not sent')
              FROM MPL m
              LEFT JOIN BoqEdit          e ON e.IdentificationKey = m.IdentificationKey AND e.SNo = m.SNo
              LEFT JOIN BoqAuthoriseItem i ON i.IdentificationKey = m.IdentificationKey AND i.SNo = m.SNo
              WHERE m.IdentificationKey = @Key
              ORDER BY m.SNo";

        public Released_BOQ()
        {
            InitializeComponent();
            Load += Released_BOQ_Load;
        }

        private async void Released_BOQ_Load(object sender, EventArgs e)
        {
            SetupBoqGrid();
            SetupDataGrid();

            btn_Search.Click -= btn_Search_Click;
            btn_Search.Click += btn_Search_Click;
            btn_Download.Click -= btn_Download_Click;
            btn_Download.Click += btn_Download_Click;

            await LoadBoqsAsync();       // shows every BOM
        }

        private void SetupBoqGrid()
        {
            DataGridView grid = dgv_MPL;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            AddTextColumn(grid, "m_SNo", "S.No.");
            AddTextColumn(grid, "m_Niap", "ID-Key");
            AddTextColumn(grid, "m_Design", "Design");
            AddTextColumn(grid, "m_Machine", "M/C Name");
            AddTextColumn(grid, "m_Status", "Status");

            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void SetupDataGrid()
        {
            DataGridView grid = dgv_Data;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            AddTextColumn(grid, "d_SNo", "S.No.");
            AddTextColumn(grid, "d_Item", "NMT Part No.");
            AddTextColumn(grid, "d_PartNo", "Part No.");
            AddTextColumn(grid, "d_Description", "Description");
            AddTextColumn(grid, "d_Type", "Type");
            AddTextColumn(grid, "d_Qty", "QTY");
            AddTextColumn(grid, "d_Status", "Status");

            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private static void AddTextColumn(DataGridView grid, string name, string header)
        {
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
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

        private async void btn_Search_Click(object sender, EventArgs e)
        {
            if (loading) return;

            if (dgv_MPL.SelectedRows.Count == 0)
            {
                dgv_Data.Rows.Clear();
                currentDataKey = "";
                return;
            }
            string key = dgv_MPL.SelectedRows[0].Cells["m_Niap"].Value?.ToString() ?? "";
            await LoadItemsAsync(key);
        }

        private async Task LoadBoqsAsync()
        {
            btn_Search.Enabled = false;
            loading = true;
            try
            {
                List<object[]> boqs = await ReadRowsAsync(BoqSql, cmd => { });

                dgv_MPL.Rows.Clear();
                dgv_Data.Rows.Clear();
                currentDataKey = "";
                foreach (object[] r in boqs)
                    dgv_MPL.Rows.Add(r[0], r[1], r[2], r[3], r[4]);

                dgv_MPL.ClearSelection();
                dgv_MPL.CurrentCell = null;
            }
            catch (Exception ex) { ShowError(ex); }
            finally
            {
                loading = false;
                btn_Search.Enabled = true;
            }
        }

        private async Task LoadItemsAsync(string key)
        {
            int request = ++itemsRequest;
            if (key == "")
            {
                dgv_Data.Rows.Clear();
                currentDataKey = "";
                return;
            }

            try
            {
                List<object[]> items = await ReadRowsAsync(ItemsSql, cmd => cmd.Parameters.AddWithValue("@Key", key));
                if (request != itemsRequest) return;      

                dgv_Data.Rows.Clear();
                int number = 1;
                foreach (object[] r in items)
                    dgv_Data.Rows.Add(number++, r[0], r[1], r[2], r[3], r[4], r[5]);
                dgv_Data.ClearSelection();
                currentDataKey = key;
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private async void btn_Download_Click(object sender, EventArgs e)
        {
            if (busy) return;
            if (dgv_MPL.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to download.");
                return;
            }

            string filePath;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FileName = "Released_BOQ.xlsx";
                if (saveDialog.ShowDialog() != DialogResult.OK) return;
                filePath = saveDialog.FileName;
            }

            string[] boqHeaders = GridHeaders(dgv_MPL);
            List<object[]> boqRows = GridRows(dgv_MPL);
            string[] itemHeaders = new[] { "NIAP No." }.Concat(GridHeaders(dgv_Data)).ToArray();
            List<object[]> itemRows = GridRows(dgv_Data)
                .Select(row => new object[] { currentDataKey }.Concat(row).ToArray()).ToList();

            busy = true;
            btn_Download.Enabled = false;
            try
            {
                await Task.Run(() => WriteExcelFile(filePath, boqHeaders, boqRows, itemHeaders, itemRows));
                MessageBox.Show("Downloaded.");
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot write the file. If it is open in Excel, close it and try again.");
            }
            catch (Exception ex) { ShowError(ex); }
            finally
            {
                busy = false;
                btn_Download.Enabled = true;
            }
        }

        private static string[] GridHeaders(DataGridView grid)
        {
            return grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Select(c => c.HeaderText).ToArray();
        }

        private static List<object[]> GridRows(DataGridView grid)
        {
            List<DataGridViewColumn> columns = grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToList();
            List<object[]> rows = new List<object[]>();
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                rows.Add(columns.Select(c => row.Cells[c.Index].Value).ToArray());
            }
            return rows;
        }

        private static void WriteExcelFile(string filePath, string[] boqHeaders, List<object[]> boqRows,
                                           string[] itemHeaders, List<object[]> itemRows)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                WriteSheet(workbook.Worksheets.Add("MPL"), boqHeaders, boqRows);
                if (itemRows.Count > 0)
                    WriteSheet(workbook.Worksheets.Add("Data"), itemHeaders, itemRows);
                workbook.SaveAs(filePath);
            }
        }

        private static void WriteSheet(IXLWorksheet sheet, string[] headers, List<object[]> rows)
        {
            for (int column = 0; column < headers.Length; column++)
                sheet.Cell(1, column + 1).Value = headers[column];

            for (int row = 0; row < rows.Count; row++)
            {
                for (int column = 0; column < rows[row].Length; column++)
                {
                    object value = rows[row][column];
                    if (value is int number)
                        sheet.Cell(row + 2, column + 1).Value = number;     
                    else
                        sheet.Cell(row + 2, column + 1).Value = value?.ToString() ?? "";
                }
            }
            sheet.Row(1).Style.Font.Bold = true;
            sheet.Columns().AdjustToContents();
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}