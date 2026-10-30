namespace Practice_ProjM
{
    partial class Assignment
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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            guna2dgv_Items = new Guna.UI2.WinForms.Guna2DataGridView();
            Serial_Number = new DataGridViewTextBoxColumn();
            Identifier = new DataGridViewTextBoxColumn();
            NIAP = new DataGridViewTextBoxColumn();
            Design = new DataGridViewTextBoxColumn();
            Unite = new DataGridViewTextBoxColumn();
            Assigned = new DataGridViewTextBoxColumn();
            panel3 = new Panel();
            btn_Clear = new Button();
            btn_Assigned = new Button();
            panel2 = new Panel();
            checkBox_HLP = new CheckBox();
            btn_Unite = new Button();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Items).BeginInit();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1474, 729);
            panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(guna2dgv_Items);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 49);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1474, 606);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Items";
            // 
            // guna2dgv_Items
            // 
            dataGridViewCellStyle4.BackColor = Color.White;
            guna2dgv_Items.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            guna2dgv_Items.BorderStyle = BorderStyle.FixedSingle;
            guna2dgv_Items.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            guna2dgv_Items.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            guna2dgv_Items.ColumnHeadersHeight = 19;
            guna2dgv_Items.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2dgv_Items.Columns.AddRange(new DataGridViewColumn[] { Serial_Number, Identifier, NIAP, Design, Unite, Assigned });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            guna2dgv_Items.DefaultCellStyle = dataGridViewCellStyle6;
            guna2dgv_Items.Dock = DockStyle.Fill;
            guna2dgv_Items.GridColor = Color.DimGray;
            guna2dgv_Items.Location = new Point(3, 19);
            guna2dgv_Items.Name = "guna2dgv_Items";
            guna2dgv_Items.RowHeadersVisible = false;
            guna2dgv_Items.Size = new Size(1468, 584);
            guna2dgv_Items.TabIndex = 0;
            guna2dgv_Items.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2dgv_Items.ThemeStyle.GridColor = Color.DimGray;
            guna2dgv_Items.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2dgv_Items.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Items.ThemeStyle.HeaderStyle.Height = 19;
            guna2dgv_Items.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Items.ThemeStyle.RowsStyle.Height = 25;
            // 
            // Serial_Number
            // 
            Serial_Number.HeaderText = "S. No.";
            Serial_Number.Name = "Serial_Number";
            // 
            // Identifier
            // 
            Identifier.HeaderText = "Identifier";
            Identifier.Name = "Identifier";
            // 
            // NIAP
            // 
            NIAP.HeaderText = "NIAP No.";
            NIAP.Name = "NIAP";
            // 
            // Design
            // 
            Design.HeaderText = "Design No.";
            Design.Name = "Design";
            // 
            // Unite
            // 
            Unite.HeaderText = "Unite";
            Unite.Name = "Unite";
            // 
            // Assigned
            // 
            Assigned.HeaderText = "Assigned";
            Assigned.Name = "Assigned";
            // 
            // panel3
            // 
            panel3.Controls.Add(btn_Unite);
            panel3.Controls.Add(btn_Clear);
            panel3.Controls.Add(btn_Assigned);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 655);
            panel3.Name = "panel3";
            panel3.Size = new Size(1474, 74);
            panel3.TabIndex = 1;
            // 
            // btn_Clear
            // 
            btn_Clear.Location = new Point(179, 24);
            btn_Clear.Name = "btn_Clear";
            btn_Clear.Size = new Size(75, 23);
            btn_Clear.TabIndex = 3;
            btn_Clear.Text = "Clear";
            btn_Clear.UseVisualStyleBackColor = true;
            btn_Clear.Click += btn_Clear_Click;
            // 
            // btn_Assigned
            // 
            btn_Assigned.Location = new Point(35, 24);
            btn_Assigned.Name = "btn_Assigned";
            btn_Assigned.Size = new Size(118, 23);
            btn_Assigned.TabIndex = 2;
            btn_Assigned.Text = "Assigned Select";
            btn_Assigned.UseVisualStyleBackColor = true;
            btn_Assigned.Click += btn_AssignedSelect_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(checkBox_HLP);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1474, 49);
            panel2.TabIndex = 0;
            // 
            // checkBox_HLP
            // 
            checkBox_HLP.AutoSize = true;
            checkBox_HLP.Location = new Point(24, 12);
            checkBox_HLP.Name = "checkBox_HLP";
            checkBox_HLP.Size = new Size(108, 19);
            checkBox_HLP.TabIndex = 4;
            checkBox_HLP.Text = "High Level Pass";
            checkBox_HLP.UseVisualStyleBackColor = true;
            // 
            // btn_Unite
            // 
            btn_Unite.Location = new Point(276, 24);
            btn_Unite.Name = "btn_Unite";
            btn_Unite.Size = new Size(75, 23);
            btn_Unite.TabIndex = 4;
            btn_Unite.Text = "Unite";
            btn_Unite.UseVisualStyleBackColor = true;
            btn_Unite.Click += btn_Unite_Click;
            // 
            // Assignment
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1474, 729);
            Controls.Add(panel1);
            Name = "Assignment";
            Text = "Assignment";
            panel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Items).EndInit();
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private Panel panel3;
        private Panel panel2;
        private CheckBox checkBox_HLP;
        private Button btn_Clear;
        private Button btn_Assigned;
        private Guna.UI2.WinForms.Guna2DataGridView guna2dgv_Items;
        private DataGridViewTextBoxColumn Serial_Number;
        private DataGridViewTextBoxColumn Identifier;
        private DataGridViewTextBoxColumn NIAP;
        private DataGridViewTextBoxColumn Design;
        private DataGridViewTextBoxColumn Unite;
        private DataGridViewTextBoxColumn Assigned;
        private Button btn_Unite;
    }
}