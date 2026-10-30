namespace Practice_ProjM
{
    partial class Viewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panel1 = new Panel();
            panel2 = new Panel();
            btn_Reject = new Button();
            btn_Select = new Button();
            groupBox2 = new GroupBox();
            guna2dgv_Result = new Guna.UI2.WinForms.Guna2DataGridView();
            Serial = new DataGridViewTextBoxColumn();
            Design = new DataGridViewTextBoxColumn();
            ID = new DataGridViewTextBoxColumn();
            Machine = new DataGridViewTextBoxColumn();
            Date = new DataGridViewTextBoxColumn();
            Note = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            btn_Search = new Button();
            dtp_DateTime = new DateTimePicker();
            label1 = new Label();
            label7 = new Label();
            txt_MachineName = new TextBox();
            label6 = new Label();
            txt_IDKey = new TextBox();
            label5 = new Label();
            txt_Design = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Result).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1474, 729);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_Reject);
            panel2.Controls.Add(btn_Select);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 656);
            panel2.Name = "panel2";
            panel2.Size = new Size(1474, 73);
            panel2.TabIndex = 2;
            // 
            // btn_Reject
            // 
            btn_Reject.Location = new Point(1312, 6);
            btn_Reject.Name = "btn_Reject";
            btn_Reject.Size = new Size(99, 36);
            btn_Reject.TabIndex = 21;
            btn_Reject.Text = "Reject";
            btn_Reject.UseVisualStyleBackColor = true;
            // 
            // btn_Select
            // 
            btn_Select.Location = new Point(1214, 6);
            btn_Select.Name = "btn_Select";
            btn_Select.Size = new Size(99, 36);
            btn_Select.TabIndex = 20;
            btn_Select.Text = "Select/Ok";
            btn_Select.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(guna2dgv_Result);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(0, 74);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1474, 582);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Result";
            // 
            // guna2dgv_Result
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            guna2dgv_Result.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            guna2dgv_Result.BorderStyle = BorderStyle.FixedSingle;
            guna2dgv_Result.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            guna2dgv_Result.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2dgv_Result.ColumnHeadersHeight = 17;
            guna2dgv_Result.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2dgv_Result.Columns.AddRange(new DataGridViewColumn[] { Serial, Design, ID, Machine, Date, Note });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2dgv_Result.DefaultCellStyle = dataGridViewCellStyle3;
            guna2dgv_Result.Dock = DockStyle.Fill;
            guna2dgv_Result.GridColor = Color.Black;
            guna2dgv_Result.Location = new Point(3, 19);
            guna2dgv_Result.Name = "guna2dgv_Result";
            guna2dgv_Result.RowHeadersVisible = false;
            guna2dgv_Result.Size = new Size(1468, 560);
            guna2dgv_Result.TabIndex = 0;
            guna2dgv_Result.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2dgv_Result.ThemeStyle.GridColor = Color.Black;
            guna2dgv_Result.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2dgv_Result.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Result.ThemeStyle.HeaderStyle.Height = 17;
            guna2dgv_Result.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Result.ThemeStyle.RowsStyle.Height = 25;
            // 
            // Serial
            // 
            Serial.HeaderText = "S. No.";
            Serial.Name = "Serial";
            // 
            // Design
            // 
            Design.HeaderText = "Design";
            Design.Name = "Design";
            // 
            // ID
            // 
            ID.HeaderText = "ID-Key";
            ID.Name = "ID";
            // 
            // Machine
            // 
            Machine.HeaderText = "M/C Name";
            Machine.Name = "Machine";
            // 
            // Date
            // 
            Date.HeaderText = "Date-Time";
            Date.Name = "Date";
            // 
            // Note
            // 
            Note.HeaderText = "Note";
            Note.Name = "Note";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btn_Search);
            groupBox1.Controls.Add(dtp_DateTime);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txt_MachineName);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txt_IDKey);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txt_Design);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1474, 74);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Search";
            // 
            // btn_Search
            // 
            btn_Search.Location = new Point(1100, 32);
            btn_Search.Name = "btn_Search";
            btn_Search.Size = new Size(75, 23);
            btn_Search.TabIndex = 19;
            btn_Search.Text = "Search";
            btn_Search.UseVisualStyleBackColor = true;
            btn_Search.Click += btn_Search_Click;
            // 
            // dtp_DateTime
            // 
            dtp_DateTime.Location = new Point(935, 32);
            dtp_DateTime.Name = "dtp_DateTime";
            dtp_DateTime.Size = new Size(143, 23);
            dtp_DateTime.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(859, 35);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 17;
            label1.Text = "Date Time";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(581, 35);
            label7.Name = "label7";
            label7.Size = new Size(66, 15);
            label7.TabIndex = 16;
            label7.Text = "M/C Name";
            // 
            // txt_MachineName
            // 
            txt_MachineName.Location = new Point(650, 32);
            txt_MachineName.Name = "txt_MachineName";
            txt_MachineName.Size = new Size(185, 23);
            txt_MachineName.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(277, 35);
            label6.Name = "label6";
            label6.Size = new Size(42, 15);
            label6.TabIndex = 14;
            label6.Text = "ID-Key";
            // 
            // txt_IDKey
            // 
            txt_IDKey.Location = new Point(382, 32);
            txt_IDKey.Name = "txt_IDKey";
            txt_IDKey.Size = new Size(163, 23);
            txt_IDKey.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 35);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 12;
            label5.Text = "Design";
            // 
            // txt_Design
            // 
            txt_Design.Location = new Point(65, 32);
            txt_Design.Name = "txt_Design";
            txt_Design.Size = new Size(173, 23);
            txt_Design.TabIndex = 11;
            // 
            // Viewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1474, 729);
            Controls.Add(panel1);
            Name = "Viewer";
            Text = "Viewer";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Result).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Label label7;
        private TextBox txt_MachineName;
        private Label label6;
        private TextBox txt_IDKey;
        private Label label5;
        private TextBox txt_Design;
        private Label label1;
        private DateTimePicker dtp_DateTime;
        private Button btn_Search;
        private Button btn_Reject;
        private Button btn_Select;
        private Guna.UI2.WinForms.Guna2DataGridView guna2dgv_Result;
        private DataGridViewTextBoxColumn Serial;
        private DataGridViewTextBoxColumn Design;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Machine;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Note;
    }
}