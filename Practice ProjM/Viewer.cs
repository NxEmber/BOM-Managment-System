using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Practice_ProjM
{
    public partial class Viewer : Form
    {
        private const string connectionString =
            @"Data Source=NMT-DXN-LT-0200\SQLExpress;Initial Catalog=BOM;Integrated Security=True;Trust Server Certificate=True;";

        public string SelectedKey { get; private set; } = "";

        private const string SearchSql =
            @"SELECT ROW_NUMBER() OVER (ORDER BY g.CreatedAt DESC) AS SNo,
                     g.Design,
                     g.IdentificationKey,
                     g.Machine,
                     g.CreatedAt,
                     ISNULL((SELECT TOP 1 n.Note FROM note n
                             WHERE n.IdentificationKey = g.IdentificationKey
                             ORDER BY n.Id DESC), '') AS Note
              FROM (SELECT IdentificationKey,
                           MAX(Design)    AS Design,
                           MAX(Machine)   AS Machine,
                           MAX(CreatedAt) AS CreatedAt
                    FROM MPL
                    WHERE (@Key     = '' OR IdentificationKey LIKE '%' + @Key + '%')
                      AND (@Design  = '' OR Design            LIKE '%' + @Design + '%')
                      AND (@Machine = '' OR Machine           LIKE '%' + @Machine + '%')
                      AND (@Date IS NULL OR CAST(CreatedAt AS date) = @Date)
                    GROUP BY IdentificationKey) g
              ORDER BY g.CreatedAt DESC";

        public Viewer()
        {
            InitializeComponent();
            Load += Viewer_Load;
        }

        private void Viewer_Load(object sender, EventArgs e)
        {
            dtp_DateTime.ShowCheckBox = true;
            dtp_DateTime.Checked = false;

            SetupResultGrid();
            guna2dgv_Result.CellDoubleClick += (s, args) =>
            {
                if (args.RowIndex >= 0) btn_Select_Click(s, EventArgs.Empty);
            };
        }

        private void SetupResultGrid()
        {
            DataGridView grid = guna2dgv_Result;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();

            grid.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "col_SNo", HeaderText = "S. No.", DataPropertyName = "SNo", Width = 70 });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "col_Design", HeaderText = "Design", DataPropertyName = "Design", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "col_Key", HeaderText = "ID-Key", DataPropertyName = "IdentificationKey", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "col_Machine", HeaderText = "M/C Name", DataPropertyName = "Machine", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "col_Date",
                HeaderText = "Date-Time",
                DataPropertyName = "CreatedAt",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd-MMM-yyyy HH:mm" }
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "col_Note", HeaderText = "Note", DataPropertyName = "Note", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private async void btn_Search_Click(object sender, EventArgs e)
        {
            string key = txt_IDKey.Text.Trim();
            string design = txt_Design.Text.Trim();
            string machine = txt_MachineName.Text.Trim();
            bool useDate = dtp_DateTime.Checked;

            if (key == "" && design == "" && machine == "" && !useDate)
            {
                MessageBox.Show("Please enter at least one search criteria.", "Input Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btn_Search.Enabled = false;
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(SearchSql, con))
                {
                    cmd.Parameters.AddWithValue("@Key", key);
                    cmd.Parameters.AddWithValue("@Design", design);
                    cmd.Parameters.AddWithValue("@Machine", machine);

                    SqlParameter dateParameter = cmd.Parameters.Add("@Date", SqlDbType.Date);
                    dateParameter.Value = useDate ? (object)dtp_DateTime.Value.Date : DBNull.Value;

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        table.Load(reader);
                }

                guna2dgv_Result.DataSource = table;
                if (table.Rows.Count == 0)
                    MessageBox.Show("No records found.", "Search");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn_Search.Enabled = true;
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = guna2dgv_Result.CurrentRow;
            if (row == null || row.IsNewRow)
            {
                MessageBox.Show("Select a row first.");
                return;
            }
            SelectedKey = row.Cells["col_Key"].Value?.ToString() ?? "";
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btn_Reject_Click(object sender, EventArgs e)
        {
            SelectedKey = "";
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}