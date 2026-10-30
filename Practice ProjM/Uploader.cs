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
    public partial class Uploader : Form
    {
        private const string connectionString =
            @"Data Source=NMT-DXN-LT-0200\SQLExpress;Initial Catalog=BOM;Integrated Security=True;Trust Server Certificate=True";

        private const int MaxTextLength = 50;
        private const int MaxMachineLength = 100;
        private const int MaxNoteLength = 100;

        private string noteText = "";
        private bool isBusy = false;
        private int selectedSerialNumber = 0;
        private string selectedIdentificationKey = "";

        private class QueryResult
        {
            public List<string> ColumnNames = new List<string>();
            public List<object[]> Rows = new List<object[]>();
        }

        private class ExcelReadResult
        {
            public List<MplRecord> Records = new List<MplRecord>();
            public List<string> Problems = new List<string>();
            public int SkippedCount = 0;
            public SheetInfo Info = new SheetInfo();   // common info from the top of the sheet
        }

        // ----------------------------- SQL -----------------------------
        private const string SelectMplSql =
     @"SELECT SNo, NMT_Serial_Number, IdentificationKey, NIAPNo,
             PN, Design, Machine, Description, Assy, Type, QTY, Rev, Customer
      FROM MPL
      WHERE (@Key = '' OR IdentificationKey = @Key)
      ORDER BY IdentificationKey, SNo";

        private const string InsertMplSql =
            @"INSERT INTO MPL
              (IdentificationKey, NIAPNo, SNo, NMT_Serial_Number,
               PN, Design, Machine, Description, Assy, Type, QTY, Rev, Customer)
              VALUES
              (@Key, @NIAPNo, @SNo, @NMT, @PN, @Design, @Machine,
               @Description, @Assy, @Type, @QTY, @Rev, @Customer)";

        private const string UpdateMplSql =
            @"UPDATE MPL
              SET NIAPNo = @NIAPNo,
              NMT_Serial_Number = @NMT,
              PN = @PN,
              Design = @Design,
              Machine = @Machine,
              Description = @Description,
              Assy = @Assy,
              Type = @Type,
              QTY = @QTY,
              Rev = @Rev,
             Customer = @Customer
             WHERE IdentificationKey = @Key AND SNo = @SNo";

        private const string DeleteRowSql = "DELETE FROM MPL WHERE IdentificationKey=@Key AND SNo=@SNo";
        private const string DeleteBomSql = "DELETE FROM MPL WHERE IdentificationKey=@Key";
        private const string CountBomSql = "SELECT COUNT(*) FROM MPL WHERE IdentificationKey=@Key";
        private const string NextSerialSql = "SELECT ISNULL(MAX(SNo), 0) + 1 FROM MPL WHERE IdentificationKey=@Key";

        private const string SelectBopSql =
            @"SELECT ROW_NUMBER() OVER (ORDER BY IdentificationKey, SNo) AS SrNo, Description, QTY
              FROM MPL WHERE (@Key = '' OR IdentificationKey = @Key) AND Type = 'BOP' ORDER BY IdentificationKey, SNo";

        private const string SelectCustomisedSql =
            @"SELECT ROW_NUMBER() OVER (ORDER BY IdentificationKey, SNo) AS SrNo, Description, QTY
              FROM MPL WHERE (@Key = '' OR IdentificationKey = @Key) AND Type LIKE 'Custom%' ORDER BY IdentificationKey, SNo";

        // ----------------------------- Form start -----------------------------
        public Uploader()
        {
            InitializeComponent();
            Load += Uploader_Load;
        }

        private async void Uploader_Load(object sender, EventArgs e)
        {
            foreach (TextBox textBox in new[] { txt_IDKey, txt_NMT_SerialNo, txt_PartNo, txt_Design, txt_Description, txt_Assy })
                textBox.MaxLength = MaxTextLength;
            txt_Machine.MaxLength = MaxMachineLength;
            txt_Customer.MaxLength = MaxTextLength;
            cb_Type.MaxLength = MaxTextLength;
            txt_QTY.MaxLength = 9;
            txt_Rev.MaxLength = 9;

            SetupMplGrid();
            dgv_MPL.AllowUserToAddRows = false;
            PrepareViewOnlyGrid(dgv_BOPItem);
            PrepareViewOnlyGrid(dgv_CustomisedItem);
            foreach (DataGridView grid in new[] { dgv_MPL, dgv_BOPItem, dgv_CustomisedItem })
                grid.DataError += (sender2, args) => args.ThrowException = false;

            btn_Browse.Click += BrowseFile;
            btn_Upload.Click += UploadExcel;
            btn_Download.Click += DownloadExcel;
            txt_IDKey.Leave += IdentificationKey_Leave;
            btn_Note.Click -= btn_Note_Click;
            btn_Note.Click += btn_Note_Click;
            SetBusy(true);
            await LoadAllGridsAsync();
            SetBusy(false);
        }

        private async void IdentificationKey_Leave(object sender, EventArgs e)
        {
            if (isBusy) return;
            selectedSerialNumber = 0;
            selectedIdentificationKey = "";
            SetBusy(true);
            await LoadAllGridsAsync();
            SetBusy(false);
        }

        private void SetupMplGrid()
        {
            dgv_MPL.AutoGenerateColumns = false;
            dgv_MPL.Columns.Clear();
            AddMplColumn("SNo", "S. No.", true);
            AddMplColumn("NIAPNo", "NIAP No.", false);
            AddMplColumn("NMT_Serial_Number", "NMT Part Number", true);
            AddMplColumn("IdentificationKey", "ID Key", false);
            AddMplColumn("PN", "Part No.", true);
            AddMplColumn("Design", "Design", false);
            AddMplColumn("Machine", "Machine", false);
            AddMplColumn("Description", "Description", true);
            AddMplColumn("Assy", "Assy", false);
            AddMplColumn("Type", "Type", true);
            AddMplColumn("QTY", "QTY", true);
            AddMplColumn("Rev", "Rev", false);
            AddMplColumn("Customer", "Customer", false);
            dgv_MPL.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void AddMplColumn(string name, string header, bool visible)
        {
            int index = dgv_MPL.Columns.Add(name, header);
            dgv_MPL.Columns[index].Visible = visible;
        }

        private void PrepareViewOnlyGrid(DataGridView grid)
        {
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();                      // always rebuild, so the names match the query

            grid.Columns.Add("SrNo", "SR. No.");
            grid.Columns.Add("Description", "Description");
            grid.Columns.Add("QTY", "QTY");
            grid.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
        }

        private void SetBusy(bool busy)
        {
            isBusy = busy;
            UseWaitCursor = busy;
        }

        private async Task LoadAllGridsAsync()
        {
            try
            {
                string key = txt_IDKey.Text.Trim();
                Task<QueryResult> mplTask = ReadQueryAsync(SelectMplSql, key);
                Task<QueryResult> bopTask = ReadQueryAsync(SelectBopSql, key);
                Task<QueryResult> customisedTask = ReadQueryAsync(SelectCustomisedSql, key);
                await Task.WhenAll(mplTask, bopTask, customisedTask);
                ShowInGrid(dgv_MPL, await mplTask);
                ShowInGrid(dgv_BOPItem, await bopTask);
                ShowInGrid(dgv_CustomisedItem, await customisedTask);
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private async Task<QueryResult> ReadQueryAsync(string sql, string key)
        {
            QueryResult result = new QueryResult();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Key", key);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        result.ColumnNames.Add(reader.GetName(i));
                    while (await reader.ReadAsync())
                    {
                        object[] rowValues = new object[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                            rowValues[i] = reader.IsDBNull(i) ? "" : reader[i];
                        result.Rows.Add(rowValues);
                    }
                }
            }
            return result;
        }


        private void ShowInGrid(DataGridView grid, QueryResult result)
        {
            grid.SuspendLayout();

            try
            {
                grid.Rows.Clear();

                if (grid.Columns.Count == 0)
                {
                    foreach (string name in result.ColumnNames)
                        grid.Columns.Add(name, name);
                }

                foreach (object[] row in result.Rows)
                {
                    object[] values = new object[grid.Columns.Count];

                    for (int i = 0; i < grid.Columns.Count; i++)
                    {
                        string columnName = grid.Columns[i].Name;

                        int sourceIndex = result.ColumnNames.FindIndex(
                            name => string.Equals(
                                name, columnName,
                                StringComparison.OrdinalIgnoreCase));

                        values[i] = sourceIndex >= 0 && sourceIndex < row.Length
                            ? row[sourceIndex]
                            : "";
                    }

                    grid.Rows.Add(values);
                }
            }
            finally
            {
                grid.ResumeLayout();
            }
        }


        private void dgv_MPL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_MPL.Rows.Count)
                return;

            DataGridViewRow row = dgv_MPL.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            int.TryParse(GetCellText(row, 0), out selectedSerialNumber);
            selectedIdentificationKey = GetCellText(row, 3);

            txt_IDKey.Text = selectedIdentificationKey;
            txt_NMT_SerialNo.Text = GetCellText(row, 2);
            txt_PartNo.Text = GetCellText(row, 4);
            txt_Design.Text = GetCellText(row, 5);
            txt_Machine.Text = GetCellText(row, 6);
            txt_Description.Text = GetCellText(row, 7);
            txt_Assy.Text = GetCellText(row, 8);
            cb_Type.Text = GetCellText(row, 9);
            txt_QTY.Text = GetCellText(row, 10);
            txt_Rev.Text = GetCellText(row, 11);
            txt_Customer.Text = GetCellText(row, 12);
        }

        private string GetCellText(DataGridViewRow row, int columnIndex)
        {
            if (columnIndex >= row.Cells.Count) return "";
            return row.Cells[columnIndex].Value?.ToString() ?? "";
        }

        private bool TryReadRecordFromForm(out MplRecord record)
        {
            record = null;
            string key = txt_IDKey.Text.Trim();
            if (key == "")
            {
                MessageBox.Show("Please enter the Identification Key.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cb_Type.Text))
            {
                MessageBox.Show("Please select a Type.");
                return false;
            }
            if (!int.TryParse(txt_QTY.Text.Trim(), out int quantity))
            {
                MessageBox.Show("QTY must be a whole number.");
                return false;
            }
            int? revision = null;
            string revisionText = txt_Rev.Text.Trim();
            if (revisionText != "")
            {
                if (!int.TryParse(revisionText, out int revisionNumber))
                {
                    MessageBox.Show("Rev must be a whole number.");
                    return false;
                }
                revision = revisionNumber;
            }
            record = new MplRecord
            {
                IdentificationKey = key,
                NmtSerialNumber = txt_NMT_SerialNo.Text.Trim(),
                PartNumber = txt_PartNo.Text.Trim(),
                Design = txt_Design.Text.Trim(),
                Machine = txt_Machine.Text.Trim(),
                Description = txt_Description.Text.Trim(),
                Assembly = txt_Assy.Text.Trim(),
                Type = cb_Type.Text,
                Quantity = quantity,
                Revision = revision,
                Customer = txt_Customer.Text.Trim()
            };
            return true;
        }

        private void ClearForm()
        {
            txt_NMT_SerialNo.Clear();
            txt_PartNo.Clear();
            txt_Design.Clear();
            txt_Machine.Clear();
            txt_Description.Clear();
            txt_Assy.Clear();
            txt_QTY.Clear();
            txt_Rev.Clear();
            txt_Customer.Clear();
            cb_Type.SelectedIndex = -1;
            selectedSerialNumber = 0;
            selectedIdentificationKey = "";
        }

        private async Task AddRecordAsync(MplRecord record)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(NextSerialSql, connection))
                {
                    command.Parameters.AddWithValue("@Key", record.IdentificationKey);
                    record.SerialNumber = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
                await RunRecordCommandAsync(connection, null, InsertMplSql, record);
            }
        }

        private void AddRecordParameters(SqlCommand command, MplRecord record)
        {
            command.Parameters.AddWithValue("@Key", record.IdentificationKey);
            command.Parameters.AddWithValue("@SNo", record.SerialNumber);
            command.Parameters.AddWithValue("@NMT", record.NmtSerialNumber);
            command.Parameters.AddWithValue("@PN", record.PartNumber);
            command.Parameters.AddWithValue("@Design", record.Design);
            command.Parameters.AddWithValue("@Machine", record.Machine);
            command.Parameters.AddWithValue("@Description", record.Description);
            command.Parameters.AddWithValue("@Assy", record.Assembly);
            command.Parameters.AddWithValue("@Type", record.Type);
            command.Parameters.AddWithValue("@QTY", record.Quantity);
            command.Parameters.AddWithValue("@Rev", (object)record.Revision ?? DBNull.Value);
            command.Parameters.AddWithValue("@Customer", record.Customer ?? "");
            command.Parameters.AddWithValue("@NIAPNo",
                string.IsNullOrWhiteSpace(record.NIAPNo)
                    ? DBNull.Value
                    : record.NIAPNo);
        }

        private async Task<int> RunRecordCommandAsync(string sql, MplRecord record)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                return await RunRecordCommandAsync(connection, null, sql, record);
            }
        }

        private async Task<int> RunRecordCommandAsync(SqlConnection connection, SqlTransaction transaction,
                                                      string sql, MplRecord record)
        {
            using (SqlCommand command = new SqlCommand(sql, connection, transaction))
            {
                AddRecordParameters(command, record);
                return await command.ExecuteNonQueryAsync();
            }
        }

        private async Task<int> RunCommandAsync(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

        private async Task<int> CountRowsAsync(string key)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(CountBomSql, connection))
            {
                command.Parameters.AddWithValue("@Key", key);
                await connection.OpenAsync();
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        private async Task ReplaceBomAsync(string key, List<MplRecord> records)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand deleteCommand = new SqlCommand(DeleteBomSql, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@Key", key);
                            await deleteCommand.ExecuteNonQueryAsync();
                        }
                        int serialNumber = 1;
                        foreach (MplRecord record in records)
                        {
                            record.IdentificationKey = key;
                            record.SerialNumber = serialNumber++;
                            await RunRecordCommandAsync(connection, transaction, InsertMplSql, record);
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private async void btn_Add_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            if (!TryReadRecordFromForm(out MplRecord record)) return;
            SetBusy(true);
            try
            {
                await AddRecordAsync(record);
                await LoadAllGridsAsync();
                ClearForm();
            }
            catch (Exception ex) { ShowError(ex); }
            finally { SetBusy(false); }
        }

        private async void btn_Edit_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            if (selectedSerialNumber == 0)
            {
                MessageBox.Show("Click a row in the MPL grid first.");
                return;
            }
            if (!TryReadRecordFromForm(out MplRecord record)) return;
            record.IdentificationKey = selectedIdentificationKey;     // the row that was clicked
            record.SerialNumber = selectedSerialNumber;
            SetBusy(true);
            try
            {
                int rowsChanged = await RunRecordCommandAsync(UpdateMplSql, record);
                if (rowsChanged == 0)
                {
                    MessageBox.Show("No record found. Check the Identification Key.");
                    return;
                }
                await LoadAllGridsAsync();
                ClearForm();
            }
            catch (Exception ex) { ShowError(ex); }
            finally { SetBusy(false); }
        }

        private async void btn_Remove_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            string key = selectedIdentificationKey;
            if (key == "" || selectedSerialNumber == 0)
            {
                MessageBox.Show("Click a row in the MPL grid first.");
                return;
            }
            DialogResult answer = MessageBox.Show("Are you sure you want to remove this record?",
                                                  "Confirm Delete", MessageBoxButtons.YesNo);
            if (answer != DialogResult.Yes) return;
            SetBusy(true);
            try
            {
                await RunCommandAsync(DeleteRowSql,
                                      new SqlParameter("@Key", key),
                                      new SqlParameter("@SNo", selectedSerialNumber));
                await LoadAllGridsAsync();
                ClearForm();
            }
            catch (Exception ex) { ShowError(ex); }
            finally { SetBusy(false); }
        }

        private void btn_Note_Click(object sender, EventArgs e)
        {
            using (Form noteWindow = new Form())
            {
                noteWindow.Text = "Note";
                noteWindow.Width = 450;
                noteWindow.Height = 300;
                noteWindow.StartPosition = FormStartPosition.CenterParent;

                TextBox noteBox = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    ScrollBars = ScrollBars.Vertical,
                    MaxLength = MaxNoteLength,
                    Text = noteText
                };
                Button okButton = new Button
                {
                    Text = "OK",
                    Dock = DockStyle.Bottom,
                    DialogResult = DialogResult.OK
                };
                noteWindow.Controls.Add(noteBox);
                noteWindow.Controls.Add(okButton);
                noteWindow.AcceptButton = okButton;
                if (noteWindow.ShowDialog(this) == DialogResult.OK)
                    noteText = noteBox.Text.Trim();
            }
        }

        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (isBusy) return;
            if (string.IsNullOrWhiteSpace(noteText))
            {
                MessageBox.Show("Saved.");
                return;
            }
            string noteKey = txt_IDKey.Text.Trim();
            if (noteKey == "")
            {
                MessageBox.Show("Enter or upload an Identification Key first, so the note can be linked to it.");
                return;
            }
            SetBusy(true);
            try
            {
                await RunCommandAsync("INSERT INTO note (IdentificationKey, Note) VALUES (@Key, @Note)",
                                      new SqlParameter("@Key", noteKey),
                                      new SqlParameter("@Note", noteText));
                noteText = "";
                MessageBox.Show("Saved.");
            }
            catch (Exception ex) { ShowError(ex); }
            finally { SetBusy(false); }
        }

        private const int FirstDataRow = 2;
        private const int MaxHeaderSearchRows = 60;       

        private enum MplField
        {
            NmtSerialNumber, PartNumber, Design, Machine, Description, Assembly, Type, Quantity, Revision, Material
        }

        private static readonly Dictionary<MplField, string[]> HeaderAliases = new Dictionary<MplField, string[]>
        {
            { MplField.NmtSerialNumber, new[] { "nmtserialno", "nmtserialnumber", "nmtserial", "nmtronicspartnumber", "nmtpartnumber", "nmtronicspartno" } },
            { MplField.PartNumber,      new[] { "partno", "partnumber", "pn" } },
            { MplField.Design,          new[] { "design" } },
            { MplField.Machine,         new[] { "machine" } },
            { MplField.Description,     new[] { "description", "desc" } },
            { MplField.Assembly,        new[] { "assembly", "assy" } },
            { MplField.Type,            new[] { "type", "bomtype" } },
            { MplField.Quantity,        new[] { "qty", "quantity" } },
            { MplField.Revision,        new[] { "rev", "revision" } },
            { MplField.Material,        new[] { "material" } }
        };

        private static readonly Dictionary<MplField, string[]> HeaderContains = new Dictionary<MplField, string[]>
        {
            { MplField.Quantity,    new[] { "qty", "quantity" } },
            { MplField.PartNumber,  new[] { "partno", "partnumber" } },
            { MplField.Description, new[] { "description" } },
            { MplField.Assembly,    new[] { "assembly" } }
        };

        private static readonly string[] ExcelHeaders =
            { "S. No.", "NMT Serial No", "ID-Key", "Part No", "Design", "Machine", "Description", "Assy", "Type", "QTY", "Rev", "Customer" };

        private void BrowseFile(object sender, EventArgs e)
        {
            if (isBusy) return;
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                fileDialog.Title = "Select the Excel file";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                    txt_Path.Text = fileDialog.FileName;
            }
        }

        private async void UploadExcel(object sender, EventArgs e)
        {
            if (isBusy) return;

            string filePath = txt_Path.Text.Trim();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Click Browse and select an Excel file first.");
                return;
            }

            string defaultType = cb_Type.Text.Trim();     // used when the file has no Type column
            SetBusy(true);
            try
            {
                ExcelReadResult excel = await Task.Run(() => ReadExcelFile(filePath, defaultType));

                if (excel.Records.Count == 0)
                {
                    string reason = excel.Problems.Count > 0
                                    ? BuildProblemText(excel.Problems)
                                    : "No usable rows were found in the file.";
                    MessageBox.Show("Nothing was uploaded.\n\n" + reason);
                    return;
                }

                // ID Key: NMT SERIAL NUMBER from the Excel -> else what is typed -> else the file name
                string niapNo = excel.Info.NIAPNo.Trim();

                if (niapNo == "")
                {
                    MessageBox.Show("NIAP No. was not found in the Excel file.");
                    return;
                }

                string key = await GenerateBomKeyAsync();

                foreach (MplRecord record in excel.Records)
                {
                    record.IdentificationKey = key;
                    record.NIAPNo = niapNo;
                }
                if (key.Length > MaxTextLength)
                {
                    MessageBox.Show("The Identification Key is longer than " + MaxTextLength + " characters.");
                    return;
                }

                if (excel.Problems.Count > 0)
                {
                    DialogResult answer = MessageBox.Show(
                        excel.Problems.Count + " row(s) have problems and will be skipped:\n\n" +
                        BuildProblemText(excel.Problems) +
                        "\n\nUpload the other " + excel.Records.Count + " row(s) anyway?",
                        "Some rows have problems", MessageBoxButtons.YesNo);
                    if (answer != DialogResult.Yes) return;
                }

                int existingRows = await CountRowsAsync(key);
                if (existingRows > 0)
                {
                    DialogResult answer = MessageBox.Show(
                        "\"" + key + "\" already has " + existingRows + " row(s).\n\n" +
                        "Replace them with the " + excel.Records.Count + " row(s) from the file?",
                        "Replace existing rows?", MessageBoxButtons.YesNo);
                    if (answer != DialogResult.Yes) return;
                }

                await ReplaceBomAsync(key, excel.Records);

                // show the common info in the form boxes (it is already saved with every row)
                txt_IDKey.Text = key;
                txt_Design.Text = excel.Info.Design;
                txt_Machine.Text = excel.Info.Machine;
                txt_Assy.Text = excel.Info.Assembly;
                txt_Customer.Text = excel.Info.Customer;
                selectedSerialNumber = 0;
                selectedIdentificationKey = "";

                await LoadAllGridsAsync();

                int bopCount = excel.Records.Count(record => record.Type == "BOP");
                MessageBox.Show("Upload finished.\nID Key: " + key +
                                "\nRows saved: " + excel.Records.Count +
                                "\n   BOP: " + bopCount +
                                "\n   Customised: " + (excel.Records.Count - bopCount) +
                                "\nSkipped (problem): " + excel.Problems.Count +
                                "\nSkipped (no description): " + excel.SkippedCount);
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot read the file. Close it in Excel and try again.");
            }
            catch (Exception ex) { ShowError(ex); }
            finally { SetBusy(false); }
        }

        private ExcelReadResult ReadExcelFile(string filePath, string defaultType)
        {
            ExcelReadResult result = new ExcelReadResult();
            int bestScore = 0;                  // most known headers found in one row so far
            string bestDescription = "";        // description of that row (shown if nothing works)
            string firstRowTexts = "";

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (XLWorkbook workbook = new XLWorkbook(fileStream))
            {
                foreach (IXLWorksheet sheet in workbook.Worksheets)
                {
                    IXLRow lastRowUsed = sheet.LastRowUsed();
                    IXLColumn lastColumnUsed = sheet.LastColumnUsed();
                    if (lastRowUsed == null || lastColumnUsed == null) continue;     // empty sheet

                    int lastRow = lastRowUsed.RowNumber();
                    int lastColumn = lastColumnUsed.ColumnNumber();
                    int searchUntilRow = Math.Min(MaxHeaderSearchRows, lastRow);

                    for (int rowNumber = 1; rowNumber <= searchUntilRow; rowNumber++)
                    {
                        Dictionary<MplField, int> columns = FindColumnsInRow(sheet, rowNumber, lastColumn);
                        if (IsHeaderRow(columns))
                        {
                            // common info (ID Key, Design, Assy, Machine, Rev) sits above the table
                            result.Info = ReadSheetInfo(sheet, rowNumber - 1, lastColumn);
                            ReadSheetRows(sheet, rowNumber, lastRow, lastColumn, columns, defaultType, result);
                            return result;
                        }
                        if (columns.Count > bestScore)
                        {
                            bestScore = columns.Count;
                            bestDescription = DescribeRow(sheet, rowNumber, lastColumn, columns);
                        }
                    }
                    if (firstRowTexts == "") firstRowTexts = ListFirstRowTexts(sheet, lastColumn);
                }
            }

            if (bestScore > 0)
                result.Problems.Add("Could not find both a Description column and a QTY column.\n\nClosest match:\n" + bestDescription);
            else
                result.Problems.Add("Could not recognise any column headers.\n\nFirst row of text in the file:\n" + firstRowTexts);
            return result;
        }

        private SheetInfo ReadSheetInfo(IXLWorksheet sheet, int lastRowToScan, int lastColumn)
        {
            SheetInfo info = new SheetInfo();
            for (int row = 1; row <= lastRowToScan; row++)
            {
                for (int column = 1; column < lastColumn; column++)
                {
                    string label = MakeSimple(sheet.Cell(row, column).GetString());
                    if (label == "") continue;

                    string value = sheet.Cell(row, column + 1).GetString().Trim();
                    if (value == "") continue;

                    switch (label)
                    {

                        case "projectdesignno":
                        case "projectdesignnumber":
                            if (info.Design == "") info.Design = value;
                            break;
                        case "mechanicalassemblynumber":
                        case "mechanicalassemblyno":
                            if (info.Assembly == "") info.Assembly = value;
                            break;
                        case "description":
                            if (info.Machine == "") info.Machine = value;
                            break;
                        case "customer":
                            if (info.Customer == "") info.Customer = value;
                            break;
                        case "revision":
                            if (info.Revision == null && TryParseWholeNumber(value, out int revisionNumber))
                                info.Revision = revisionNumber;
                            break;
                        case "niapno":
                        case "niapnumber":
                        case "niap":
                        case "niapserialnumber":
                        case "nmtserialnumber":
                            info.NIAPNo = value;
                            break;
                    }
                }
            }
            return info;
        }

        private bool IsHeaderRow(Dictionary<MplField, int> columns)
        {
            return columns.ContainsKey(MplField.Description) && columns.ContainsKey(MplField.Quantity);
        }

        private string MakeSimple(string text)
        {
            return new string(text.Where(char.IsLetterOrDigit).ToArray()).ToLower();
        }

        private Dictionary<MplField, int> FindColumnsInRow(IXLWorksheet sheet, int rowNumber, int lastColumn)
        {
            Dictionary<MplField, int> columns = new Dictionary<MplField, int>();
            for (int columnNumber = 1; columnNumber <= lastColumn; columnNumber++)
            {
                string header = MakeSimple(sheet.Cell(rowNumber, columnNumber).GetString());
                if (header == "") continue;
                if (FindFieldForHeader(header, out MplField field) && !columns.ContainsKey(field))
                    columns[field] = columnNumber;
            }
            return columns;
        }

        private bool FindFieldForHeader(string header, out MplField field)
        {
            foreach (KeyValuePair<MplField, string[]> alias in HeaderAliases)
            {
                if (alias.Value.Contains(header))
                {
                    field = alias.Key;
                    return true;
                }
            }
            foreach (KeyValuePair<MplField, string[]> part in HeaderContains)
            {
                string[] words = part.Value;
                if (words.Any(word => header.Contains(word)))
                {
                    field = part.Key;
                    return true;
                }
            }
            field = MplField.Description;
            return false;
        }

        private string DescribeRow(IXLWorksheet sheet, int rowNumber, int lastColumn, Dictionary<MplField, int> columns)
        {
            List<string> missing = new List<string>();
            if (!columns.ContainsKey(MplField.Description)) missing.Add("Description");
            if (!columns.ContainsKey(MplField.Quantity)) missing.Add("QTY");
            return "Sheet \"" + sheet.Name + "\", row " + rowNumber + "\n" +
                   "Missing: " + string.Join(", ", missing) + "\n" +
                   "Headers in that row: " + ListRowTexts(sheet, rowNumber, lastColumn);
        }

        private string ListRowTexts(IXLWorksheet sheet, int rowNumber, int lastColumn)
        {
            List<string> texts = new List<string>();
            for (int columnNumber = 1; columnNumber <= lastColumn; columnNumber++)
            {
                string text = sheet.Cell(rowNumber, columnNumber).GetString().Trim();
                if (text != "") texts.Add(text);
            }
            return string.Join(", ", texts);
        }

        private string ListFirstRowTexts(IXLWorksheet sheet, int lastColumn)
        {
            for (int rowNumber = 1; rowNumber <= MaxHeaderSearchRows; rowNumber++)
            {
                string texts = ListRowTexts(sheet, rowNumber, lastColumn);
                if (texts != "") return texts;
            }
            return "(none)";
        }

        private void ReadSheetRows(IXLWorksheet sheet, int headerRow, int lastRow, int lastColumn,
                                   Dictionary<MplField, int> columns, string defaultType, ExcelReadResult result)
        {
            for (int rowNumber = headerRow + 1; rowNumber <= lastRow; rowNumber++)
            {
                Dictionary<MplField, int> newColumns = FindColumnsInRow(sheet, rowNumber, lastColumn);
                if (IsHeaderRow(newColumns))
                {
                    columns = newColumns;
                    continue;
                }

                if (IsExcelRowEmpty(sheet, rowNumber, columns)) continue;

                if (GetField(sheet, rowNumber, columns, MplField.Description) == "")
                {
                    result.SkippedCount++;
                    continue;
                }

                string problem = ReadExcelRow(sheet, rowNumber, columns, defaultType, result.Info, out MplRecord record);
                if (problem != "")
                    result.Problems.Add("Row " + rowNumber + ": " + problem);
                else
                    result.Records.Add(record);
            }
        }

        private bool IsExcelRowEmpty(IXLWorksheet sheet, int rowNumber, Dictionary<MplField, int> columns)
        {
            foreach (int columnNumber in columns.Values)
                if (sheet.Cell(rowNumber, columnNumber).GetString().Trim() != "")
                    return false;
            return true;
        }

        private string GetField(IXLWorksheet sheet, int rowNumber, Dictionary<MplField, int> columns, MplField field)
        {
            if (!columns.TryGetValue(field, out int columnNumber)) return "";
            return sheet.Cell(rowNumber, columnNumber).GetString().Trim();
        }

        private string ReadExcelRow(IXLWorksheet sheet, int rowNumber, Dictionary<MplField, int> columns,
                                    string defaultType, SheetInfo info, out MplRecord record)
        {
            record = null;

            string nmtSerialNumber = GetField(sheet, rowNumber, columns, MplField.NmtSerialNumber);
            string partNumber = GetField(sheet, rowNumber, columns, MplField.PartNumber);
            string design = GetField(sheet, rowNumber, columns, MplField.Design);
            string machine = GetField(sheet, rowNumber, columns, MplField.Machine);
            string description = GetField(sheet, rowNumber, columns, MplField.Description);
            string assembly = GetField(sheet, rowNumber, columns, MplField.Assembly);
            string type = GetField(sheet, rowNumber, columns, MplField.Type);
            string material = GetField(sheet, rowNumber, columns, MplField.Material);
            string quantityText = GetField(sheet, rowNumber, columns, MplField.Quantity);
            string revisionText = GetField(sheet, rowNumber, columns, MplField.Revision);

            // common info from the top of the sheet, used when the row has no value of its own
            if (design == "") design = info.Design;
            if (machine == "") machine = info.Machine;
            if (assembly == "") assembly = info.Assembly;
            string customer = info.Customer;

            type = DecideType(type, material);
            if (type == "") type = defaultType;
            if (type == "")
                return "Type is empty (add a Type column to the file, or choose a Type in the form).";

            if (!TryParseWholeNumber(quantityText, out int quantity))
                return "QTY must be a whole number.";

            int? revision = info.Revision;
            if (revisionText != "")
            {
                if (!TryParseWholeNumber(revisionText, out int revisionNumber))
                    return "Rev must be a whole number (or empty).";
                revision = revisionNumber;
            }

            string[] textValues = { nmtSerialNumber, partNumber, design, description, assembly, type, customer };
            if (textValues.Any(text => text.Length > MaxTextLength))
                return "A text value is longer than " + MaxTextLength + " characters.";
            if (machine.Length > MaxMachineLength)
                return "Machine name is longer than " + MaxMachineLength + " characters.";

            record = new MplRecord
            {
                NmtSerialNumber = nmtSerialNumber,
                PartNumber = partNumber,
                Design = design,
                Machine = machine,
                Description = description,
                Assembly = assembly,
                Type = type,
                Quantity = quantity,
                Revision = revision,
                Customer = customer
            };
            return "";
        }

        private string DecideType(string type, string material)
        {
            string text = MakeSimple(type + " " + material);     // "STD/B.O.P." -> "stdbop"
            if (text.Contains("bop") || text.Contains("boughtout"))
                return "BOP";
            return type;
        }

        private bool TryParseWholeNumber(string text, out int number)
        {
            if (int.TryParse(text, out number)) return true;
            if (double.TryParse(text, out double decimalNumber) &&
                decimalNumber == Math.Floor(decimalNumber) &&
                decimalNumber >= int.MinValue && decimalNumber <= int.MaxValue)
            {
                number = (int)decimalNumber;
                return true;
            }
            number = 0;
            return false;
        }

        private string BuildProblemText(List<string> problems)
        {
            string text = string.Join("\n", problems.Take(10));
            if (problems.Count > 10)
                text += "\n...and " + (problems.Count - 10) + " more.";
            return text;
        }

        private async void DownloadExcel(object sender, EventArgs e)
        {
            if (isBusy) return;

            string filePath;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FileName = "MPL.xlsx";
                if (saveDialog.ShowDialog() != DialogResult.OK) return;
                filePath = saveDialog.FileName;
            }

            List<object[]> gridRows = GetMplGridRows();
            SetBusy(true);
            try
            {
                await Task.Run(() => WriteExcelFile(filePath, gridRows));
                txt_Path.Text = filePath;
                MessageBox.Show("Downloaded.");
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot write the file. If it is open in Excel, close it and try again.");
            }
            catch (Exception ex) { ShowError(ex); }
            finally { SetBusy(false); }
        }
        private List<object[]> GetMplGridRows()
        {
            List<object[]> rows = new List<object[]>();

            foreach (DataGridViewRow row in dgv_MPL.Rows)
            {
                if (row.IsNewRow) continue;

                rows.Add(new object[]
                {
            row.Cells["SNo"].Value,
            row.Cells["NMT_Serial_Number"].Value,
            row.Cells["IdentificationKey"].Value,
            row.Cells["PN"].Value,
            row.Cells["Design"].Value,
            row.Cells["Machine"].Value,
            row.Cells["Description"].Value,
            row.Cells["Assy"].Value,
            row.Cells["Type"].Value,
            row.Cells["QTY"].Value,
            row.Cells["Rev"].Value,
            row.Cells["Customer"].Value
                });
            }

            return rows;
        }

        private void WriteExcelFile(string filePath, List<object[]> gridRows)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet sheet = workbook.Worksheets.Add("MPL");
                for (int column = 0; column < ExcelHeaders.Length; column++)
                    sheet.Cell(1, column + 1).Value = ExcelHeaders[column];

                int excelRow = FirstDataRow;
                foreach (object[] values in gridRows)
                {
                    for (int column = 0; column < values.Length; column++)
                    {
                        object cellValue = values[column];
                        if (cellValue is int number)
                            sheet.Cell(excelRow, column + 1).Value = number;      // keep numbers as numbers
                        else
                            sheet.Cell(excelRow, column + 1).Value = cellValue?.ToString() ?? "";
                    }
                    excelRow++;
                }
                sheet.Row(1).Style.Font.Bold = true;
                sheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        private async Task<string> GenerateBomKeyAsync()
        {
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(
                "SELECT NEXT VALUE FOR dbo.BOMKeySequence", con);

            await con.OpenAsync();

            object result = await cmd.ExecuteScalarAsync();

            if (result == null || result == DBNull.Value)
                throw new Exception("Failed to generate BOM key.");

            int number = Convert.ToInt32(result);

            return "M" + number.ToString("D5");
        }
    }

    public class SheetInfo
    {
        public string IdKey = "";       // NMT SERIAL NUMBER   (for example NIAP 2624-25)
        public string Design = "";      // PROJECT DESIGN NO.  (for example 1020)
        public string Assembly = "";    // MECHANICAL ASSEMBLY NUMBER (for example 69-21)
        public string Machine = "";     // DESCRIPTION in the top block (the machine name)
        public int? Revision;           // REVISION
        public string Customer = "";    // CUSTOMER           (for example HYDRA)
        public string NIAPNo = "";
    }

    public class MplRecord
    {
        public string IdentificationKey;   // the BOM this row belongs to (the Identification Key box)
        public int SerialNumber;           // S. No. = row number inside that BOM
        public string NmtSerialNumber;     // NMT part number (for example 110-10200000-0-002188-1)
        public string PartNumber;          // Part No.
        public string Design;
        public string Machine;
        public string Description;
        public string Assembly;
        public string Type;                // BOP or CUSTOMIZED
        public int Quantity;
        public int? Revision;              // int? = can be empty
        public string Customer;            // CUSTOMER from the Excel top block
        public string NIAPNo;
    }
}