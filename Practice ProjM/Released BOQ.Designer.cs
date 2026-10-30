namespace Practice_ProjM
{
    partial class Released_BOQ
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            panel4 = new Panel();
            Data = new GroupBox();
            dgv_Data = new Guna.UI2.WinForms.Guna2DataGridView();
            panel3 = new Panel();
            MPL = new GroupBox();
            btn_Search = new Button();
            btn_Download = new Button();
            dgv_MPL = new Guna.UI2.WinForms.Guna2DataGridView();
            SNo = new DataGridViewTextBoxColumn();
            NIAP = new DataGridViewTextBoxColumn();
            Design = new DataGridViewTextBoxColumn();
            Machine = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            panel2 = new Panel();
            Serial = new DataGridViewTextBoxColumn();
            NMTPart = new DataGridViewTextBoxColumn();
            Part = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            Type = new DataGridViewTextBoxColumn();
            QTY = new DataGridViewTextBoxColumn();
            Stats = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            Data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_Data).BeginInit();
            panel3.SuspendLayout();
            MPL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_MPL).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1474, 729);
            panel1.TabIndex = 0;
            // 
            // panel4
            // 
            panel4.Controls.Add(Data);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 367);
            panel4.Name = "panel4";
            panel4.Size = new Size(1474, 362);
            panel4.TabIndex = 2;
            // 
            // Data
            // 
            Data.Controls.Add(dgv_Data);
            Data.Dock = DockStyle.Fill;
            Data.Location = new Point(0, 0);
            Data.Name = "Data";
            Data.Size = new Size(1474, 362);
            Data.TabIndex = 0;
            Data.TabStop = false;
            Data.Text = "Data";
            // 
            // dgv_Data
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            dgv_Data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgv_Data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgv_Data.ColumnHeadersHeight = 17;
            dgv_Data.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv_Data.Columns.AddRange(new DataGridViewColumn[] { Serial, NMTPart, Part, Description, Type, QTY, Stats });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgv_Data.DefaultCellStyle = dataGridViewCellStyle3;
            dgv_Data.Dock = DockStyle.Fill;
            dgv_Data.GridColor = Color.FromArgb(231, 229, 255);
            dgv_Data.Location = new Point(3, 19);
            dgv_Data.Name = "dgv_Data";
            dgv_Data.RowHeadersVisible = false;
            dgv_Data.Size = new Size(1468, 340);
            dgv_Data.TabIndex = 0;
            dgv_Data.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgv_Data.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgv_Data.ThemeStyle.HeaderStyle.Height = 17;
            dgv_Data.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgv_Data.ThemeStyle.RowsStyle.Height = 25;
            // 
            // panel3
            // 
            panel3.Controls.Add(MPL);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 63);
            panel3.Name = "panel3";
            panel3.Size = new Size(1474, 304);
            panel3.TabIndex = 1;
            // 
            // MPL
            // 
            MPL.Controls.Add(btn_Search);
            MPL.Controls.Add(btn_Download);
            MPL.Controls.Add(dgv_MPL);
            MPL.Dock = DockStyle.Fill;
            MPL.Location = new Point(0, 0);
            MPL.Name = "MPL";
            MPL.Size = new Size(1474, 304);
            MPL.TabIndex = 0;
            MPL.TabStop = false;
            MPL.Text = "MLP";
            // 
            // btn_Search
            // 
            btn_Search.Location = new Point(152, 22);
            btn_Search.Name = "btn_Search";
            btn_Search.Size = new Size(75, 23);
            btn_Search.TabIndex = 2;
            btn_Search.Text = "Search";
            btn_Search.UseVisualStyleBackColor = true;
            // 
            // btn_Download
            // 
            btn_Download.Location = new Point(33, 22);
            btn_Download.Name = "btn_Download";
            btn_Download.Size = new Size(75, 23);
            btn_Download.TabIndex = 1;
            btn_Download.Text = "Download";
            btn_Download.UseVisualStyleBackColor = true;
            // 
            // dgv_MPL
            // 
            dataGridViewCellStyle4.BackColor = Color.White;
            dgv_MPL.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgv_MPL.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgv_MPL.ColumnHeadersHeight = 17;
            dgv_MPL.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv_MPL.Columns.AddRange(new DataGridViewColumn[] { SNo, NIAP, Design, Machine, Status });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgv_MPL.DefaultCellStyle = dataGridViewCellStyle6;
            dgv_MPL.Dock = DockStyle.Bottom;
            dgv_MPL.GridColor = Color.FromArgb(231, 229, 255);
            dgv_MPL.Location = new Point(3, 59);
            dgv_MPL.Name = "dgv_MPL";
            dgv_MPL.RowHeadersVisible = false;
            dgv_MPL.Size = new Size(1468, 242);
            dgv_MPL.TabIndex = 0;
            dgv_MPL.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgv_MPL.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgv_MPL.ThemeStyle.HeaderStyle.Height = 17;
            dgv_MPL.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgv_MPL.ThemeStyle.RowsStyle.Height = 25;
            // 
            // SNo
            // 
            SNo.HeaderText = "S.No.";
            SNo.Name = "SNo";
            // 
            // NIAP
            // 
            NIAP.HeaderText = "NIAP No.";
            NIAP.Name = "NIAP";
            // 
            // Design
            // 
            Design.HeaderText = "Design";
            Design.Name = "Design";
            // 
            // Machine
            // 
            Machine.HeaderText = "M/C Name";
            Machine.Name = "Machine";
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1474, 63);
            panel2.TabIndex = 0;
            // 
            // Serial
            // 
            Serial.HeaderText = "S.No.";
            Serial.Name = "Serial";
            // 
            // NMTPart
            // 
            NMTPart.HeaderText = "NMT Part No.";
            NMTPart.Name = "NMTPart";
            // 
            // Part
            // 
            Part.HeaderText = "Part No.";
            Part.Name = "Part";
            // 
            // Description
            // 
            Description.HeaderText = "Description";
            Description.Name = "Description";
            // 
            // Type
            // 
            Type.HeaderText = "Type";
            Type.Name = "Type";
            // 
            // QTY
            // 
            QTY.HeaderText = "QTY";
            QTY.Name = "QTY";
            // 
            // Stats
            // 
            Stats.HeaderText = "Status";
            Stats.Name = "Stats";
            // 
            // Released_BOQ
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1474, 729);
            Controls.Add(panel1);
            Name = "Released_BOQ";
            Text = "Released_BOQ";
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            Data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_Data).EndInit();
            panel3.ResumeLayout(false);
            MPL.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_MPL).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
        private GroupBox Data;
        private GroupBox MPL;
        private Guna.UI2.WinForms.Guna2DataGridView dgv_Data;
        private Guna.UI2.WinForms.Guna2DataGridView dgv_MPL;
        private Button btn_Search;
        private Button btn_Download;
        private DataGridViewTextBoxColumn SNo;
        private DataGridViewTextBoxColumn NIAP;
        private DataGridViewTextBoxColumn Design;
        private DataGridViewTextBoxColumn Machine;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn Serial;
        private DataGridViewTextBoxColumn NMTPart;
        private DataGridViewTextBoxColumn Part;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewTextBoxColumn QTY;
        private DataGridViewTextBoxColumn Stats;
    }
}