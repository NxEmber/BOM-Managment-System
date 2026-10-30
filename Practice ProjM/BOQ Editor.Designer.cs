namespace Practice_ProjM
{
    partial class BOQ_Editor
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            panel1 = new Panel();
            panel3 = new Panel();
            btn_ForApproval = new Button();
            btn_Save = new Button();
            btn_Revised = new Button();
            groupBox2 = new GroupBox();
            guna2dgv_Status = new Guna.UI2.WinForms.Guna2DataGridView();
            Serial = new DataGridViewTextBoxColumn();
            Item1 = new DataGridViewTextBoxColumn();
            oldQTY = new DataGridViewTextBoxColumn();
            newQTY = new DataGridViewTextBoxColumn();
            Status1 = new DataGridViewTextBoxColumn();
            Remark = new DataGridViewTextBoxColumn();
            label3 = new Label();
            btn_PA = new Button();
            btn_O = new Button();
            btn_R = new Button();
            btn_V = new Button();
            btn_Minus = new Button();
            btn_Plus = new Button();
            groupBox1 = new GroupBox();
            guna2dgv_Data = new Guna.UI2.WinForms.Guna2DataGridView();
            Check = new DataGridViewCheckBoxColumn();
            SerialNum = new DataGridViewTextBoxColumn();
            Mechanical = new DataGridViewTextBoxColumn();
            DesignNum = new DataGridViewTextBoxColumn();
            Description1 = new DataGridViewTextBoxColumn();
            Custom = new DataGridViewTextBoxColumn();
            BOP = new DataGridViewTextBoxColumn();
            panel2 = new Panel();
            btn_Add = new Button();
            cb_Status = new ComboBox();
            label2 = new Label();
            cb_View = new ComboBox();
            label1 = new Label();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Status).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Data).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1474, 729);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(btn_ForApproval);
            panel3.Controls.Add(btn_Save);
            panel3.Controls.Add(btn_Revised);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 661);
            panel3.Name = "panel3";
            panel3.Size = new Size(1474, 68);
            panel3.TabIndex = 3;
            // 
            // btn_ForApproval
            // 
            btn_ForApproval.Location = new Point(1288, 6);
            btn_ForApproval.Name = "btn_ForApproval";
            btn_ForApproval.Size = new Size(102, 23);
            btn_ForApproval.TabIndex = 4;
            btn_ForApproval.Text = "For Approval →";
            btn_ForApproval.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            btn_Save.Location = new Point(1178, 6);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(75, 23);
            btn_Save.TabIndex = 3;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            // 
            // btn_Revised
            // 
            btn_Revised.Location = new Point(1074, 6);
            btn_Revised.Name = "btn_Revised";
            btn_Revised.Size = new Size(75, 23);
            btn_Revised.TabIndex = 2;
            btn_Revised.Text = "Revised";
            btn_Revised.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(guna2dgv_Status);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(btn_PA);
            groupBox2.Controls.Add(btn_O);
            groupBox2.Controls.Add(btn_R);
            groupBox2.Controls.Add(btn_V);
            groupBox2.Controls.Add(btn_Minus);
            groupBox2.Controls.Add(btn_Plus);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(0, 327);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1474, 334);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Status";
            // 
            // guna2dgv_Status
            // 
            dataGridViewCellStyle7.BackColor = Color.White;
            guna2dgv_Status.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            guna2dgv_Status.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            guna2dgv_Status.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            guna2dgv_Status.ColumnHeadersHeight = 17;
            guna2dgv_Status.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2dgv_Status.Columns.AddRange(new DataGridViewColumn[] { Serial, Item1, oldQTY, newQTY, Status1, Remark });
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.White;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            guna2dgv_Status.DefaultCellStyle = dataGridViewCellStyle9;
            guna2dgv_Status.Dock = DockStyle.Bottom;
            guna2dgv_Status.GridColor = Color.Black;
            guna2dgv_Status.Location = new Point(3, 51);
            guna2dgv_Status.Name = "guna2dgv_Status";
            guna2dgv_Status.RowHeadersVisible = false;
            guna2dgv_Status.Size = new Size(1468, 280);
            guna2dgv_Status.TabIndex = 8;
            guna2dgv_Status.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2dgv_Status.ThemeStyle.GridColor = Color.Black;
            guna2dgv_Status.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2dgv_Status.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Status.ThemeStyle.HeaderStyle.Height = 17;
            guna2dgv_Status.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Status.ThemeStyle.RowsStyle.Height = 25;
            // 
            // Serial
            // 
            Serial.HeaderText = "S.No.";
            Serial.Name = "Serial";
            // 
            // Item1
            // 
            Item1.HeaderText = "Item";
            Item1.Name = "Item1";
            // 
            // oldQTY
            // 
            oldQTY.HeaderText = "Old Qty";
            oldQTY.Name = "oldQTY";
            // 
            // newQTY
            // 
            newQTY.HeaderText = "New Qty";
            newQTY.Name = "newQTY";
            // 
            // Status1
            // 
            Status1.HeaderText = "Status";
            Status1.Name = "Status1";
            // 
            // Remark
            // 
            Remark.HeaderText = "Remarks";
            Remark.Name = "Remark";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(208, 26);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 7;
            label3.Text = "Mark as:";
            // 
            // btn_PA
            // 
            btn_PA.BackColor = Color.Yellow;
            btn_PA.Location = new Point(511, 22);
            btn_PA.Name = "btn_PA";
            btn_PA.Size = new Size(75, 23);
            btn_PA.TabIndex = 6;
            btn_PA.Text = "PA";
            btn_PA.UseVisualStyleBackColor = false;
            // 
            // btn_O
            // 
            btn_O.BackColor = Color.LightBlue;
            btn_O.Location = new Point(430, 22);
            btn_O.Name = "btn_O";
            btn_O.Size = new Size(75, 23);
            btn_O.TabIndex = 5;
            btn_O.Text = "O";
            btn_O.UseVisualStyleBackColor = false;
            // 
            // btn_R
            // 
            btn_R.BackColor = Color.FromArgb(255, 128, 128);
            btn_R.Location = new Point(349, 22);
            btn_R.Name = "btn_R";
            btn_R.Size = new Size(75, 23);
            btn_R.TabIndex = 4;
            btn_R.Text = "R";
            btn_R.UseVisualStyleBackColor = false;
            // 
            // btn_V
            // 
            btn_V.BackColor = Color.LightGreen;
            btn_V.Location = new Point(268, 22);
            btn_V.Name = "btn_V";
            btn_V.Size = new Size(75, 23);
            btn_V.TabIndex = 3;
            btn_V.Text = "V";
            btn_V.UseVisualStyleBackColor = false;
            // 
            // btn_Minus
            // 
            btn_Minus.Font = new Font("SimSun", 12F, FontStyle.Bold);
            btn_Minus.ForeColor = Color.Red;
            btn_Minus.Location = new Point(93, 22);
            btn_Minus.Name = "btn_Minus";
            btn_Minus.Size = new Size(75, 23);
            btn_Minus.TabIndex = 2;
            btn_Minus.Text = "-";
            btn_Minus.UseVisualStyleBackColor = true;
            // 
            // btn_Plus
            // 
            btn_Plus.Font = new Font("SimSun", 12F, FontStyle.Bold);
            btn_Plus.Location = new Point(12, 22);
            btn_Plus.Name = "btn_Plus";
            btn_Plus.Size = new Size(75, 23);
            btn_Plus.TabIndex = 1;
            btn_Plus.Text = "+";
            btn_Plus.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(guna2dgv_Data);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 69);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1474, 258);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Data";
            // 
            // guna2dgv_Data
            // 
            dataGridViewCellStyle10.BackColor = Color.White;
            guna2dgv_Data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            guna2dgv_Data.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle11.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle11.ForeColor = Color.White;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.True;
            guna2dgv_Data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            guna2dgv_Data.ColumnHeadersHeight = 17;
            guna2dgv_Data.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2dgv_Data.Columns.AddRange(new DataGridViewColumn[] { Check, SerialNum, Mechanical, DesignNum, Description1, Custom, BOP });
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = Color.White;
            dataGridViewCellStyle12.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle12.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle12.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle12.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.False;
            guna2dgv_Data.DefaultCellStyle = dataGridViewCellStyle12;
            guna2dgv_Data.Dock = DockStyle.Fill;
            guna2dgv_Data.GridColor = Color.Black;
            guna2dgv_Data.Location = new Point(3, 19);
            guna2dgv_Data.Name = "guna2dgv_Data";
            guna2dgv_Data.RowHeadersVisible = false;
            guna2dgv_Data.Size = new Size(1468, 236);
            guna2dgv_Data.TabIndex = 8;
            guna2dgv_Data.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2dgv_Data.ThemeStyle.GridColor = Color.Black;
            guna2dgv_Data.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2dgv_Data.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Data.ThemeStyle.HeaderStyle.Height = 17;
            guna2dgv_Data.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Data.ThemeStyle.RowsStyle.Height = 25;
            // 
            // Check
            // 
            Check.HeaderText = "";
            Check.Name = "Check";
            // 
            // SerialNum
            // 
            SerialNum.HeaderText = "S. No.";
            SerialNum.Name = "SerialNum";
            // 
            // Mechanical
            // 
            Mechanical.HeaderText = "Mechanical Item";
            Mechanical.Name = "Mechanical";
            // 
            // DesignNum
            // 
            DesignNum.HeaderText = "Design No.";
            DesignNum.Name = "DesignNum";
            // 
            // Description1
            // 
            Description1.HeaderText = "Description";
            Description1.Name = "Description1";
            // 
            // Custom
            // 
            Custom.HeaderText = "Custom";
            Custom.Name = "Custom";
            // 
            // BOP
            // 
            BOP.HeaderText = "BOP";
            BOP.Name = "BOP";
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_Add);
            panel2.Controls.Add(cb_Status);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(cb_View);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1474, 69);
            panel2.TabIndex = 0;
            // 
            // btn_Add
            // 
            btn_Add.Location = new Point(463, 33);
            btn_Add.Name = "btn_Add";
            btn_Add.Size = new Size(75, 23);
            btn_Add.TabIndex = 8;
            btn_Add.Text = "Add Item";
            btn_Add.UseVisualStyleBackColor = true;
            btn_Add.Click += btn_Add_Click;
            // 
            // cb_Status
            // 
            cb_Status.FormattingEnabled = true;
            cb_Status.Location = new Point(293, 33);
            cb_Status.Name = "cb_Status";
            cb_Status.Size = new Size(121, 23);
            cb_Status.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(248, 36);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 6;
            label2.Text = "Status";
            // 
            // cb_View
            // 
            cb_View.FormattingEnabled = true;
            cb_View.Location = new Point(70, 33);
            cb_View.Name = "cb_View";
            cb_View.Size = new Size(121, 23);
            cb_View.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 36);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 4;
            label1.Text = "View";
            // 
            // BOQ_Editor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1474, 729);
            Controls.Add(panel1);
            Name = "BOQ_Editor";
            Text = "BOQ_Editor";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Status).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Data).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private ComboBox cb_Status;
        private Label label2;
        private ComboBox cb_View;
        private Label label1;
        private Panel panel3;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Button btn_Plus;
        private Button btn_PA;
        private Button btn_O;
        private Button btn_R;
        private Button btn_V;
        private Button btn_Minus;
        private Button btn_ForApproval;
        private Button btn_Save;
        private Button btn_Revised;
        private Label label3;
        private Guna.UI2.WinForms.Guna2DataGridView guna2dgv_Data;
        private DataGridViewCheckBoxColumn Check;
        private DataGridViewTextBoxColumn SerialNum;
        private DataGridViewTextBoxColumn Mechanical;
        private DataGridViewTextBoxColumn DesignNum;
        private DataGridViewTextBoxColumn Description1;
        private DataGridViewTextBoxColumn Custom;
        private DataGridViewTextBoxColumn BOP;
        private Guna.UI2.WinForms.Guna2DataGridView guna2dgv_Status;
        private DataGridViewTextBoxColumn Serial;
        private DataGridViewTextBoxColumn Item1;
        private DataGridViewTextBoxColumn oldQTY;
        private DataGridViewTextBoxColumn newQTY;
        private DataGridViewTextBoxColumn Status1;
        private DataGridViewTextBoxColumn Remark;
        private Button btn_Add;
    }
}