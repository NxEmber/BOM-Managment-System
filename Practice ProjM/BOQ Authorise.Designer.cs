namespace Practice_ProjM
{
    partial class BOQ_Authorise
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
            panel3 = new Panel();
            btn_Reject = new Button();
            btn_Approve = new Button();
            groupBox2 = new GroupBox();
            guna2dgv_Items = new Guna.UI2.WinForms.Guna2DataGridView();
            SrNo = new DataGridViewTextBoxColumn();
            NIAPNum = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            guna2dgv_BOQ = new Guna.UI2.WinForms.Guna2DataGridView();
            Chk_Box = new DataGridViewCheckBoxColumn();
            SNo = new DataGridViewTextBoxColumn();
            WorkOrder = new DataGridViewTextBoxColumn();
            MachineName = new DataGridViewTextBoxColumn();
            DesignNo = new DataGridViewTextBoxColumn();
            Stats = new DataGridViewTextBoxColumn();
            BOM = new DataGridViewTextBoxColumn();
            BOQ = new DataGridViewTextBoxColumn();
            UploadBy = new DataGridViewTextBoxColumn();
            DateTime = new DataGridViewTextBoxColumn();
            Custom = new DataGridViewTextBoxColumn();
            BOP = new DataGridViewTextBoxColumn();
            panel2 = new Panel();
            rb_Released = new RadioButton();
            rb_Pending = new RadioButton();
            label2 = new Label();
            cb_Type = new ComboBox();
            label1 = new Label();
            panel3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Items).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2dgv_BOQ).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.Controls.Add(btn_Reject);
            panel3.Controls.Add(btn_Approve);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 637);
            panel3.Name = "panel3";
            panel3.Size = new Size(1474, 92);
            panel3.TabIndex = 7;
            // 
            // btn_Reject
            // 
            btn_Reject.Location = new Point(1139, 16);
            btn_Reject.Name = "btn_Reject";
            btn_Reject.Size = new Size(75, 23);
            btn_Reject.TabIndex = 3;
            btn_Reject.Text = "Reject";
            btn_Reject.UseVisualStyleBackColor = true;
            // 
            // btn_Approve
            // 
            btn_Approve.Location = new Point(1043, 16);
            btn_Approve.Name = "btn_Approve";
            btn_Approve.Size = new Size(75, 23);
            btn_Approve.TabIndex = 2;
            btn_Approve.Text = "Approve";
            btn_Approve.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(guna2dgv_Items);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(0, 358);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1474, 279);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Items";
            // 
            // guna2dgv_Items
            // 
            dataGridViewCellStyle1.BackColor = Color.White;
            guna2dgv_Items.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            guna2dgv_Items.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            guna2dgv_Items.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2dgv_Items.ColumnHeadersHeight = 17;
            guna2dgv_Items.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2dgv_Items.Columns.AddRange(new DataGridViewColumn[] { SrNo, NIAPNum, Description, Quantity, Status });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2dgv_Items.DefaultCellStyle = dataGridViewCellStyle3;
            guna2dgv_Items.Dock = DockStyle.Fill;
            guna2dgv_Items.GridColor = Color.Black;
            guna2dgv_Items.Location = new Point(3, 19);
            guna2dgv_Items.Name = "guna2dgv_Items";
            guna2dgv_Items.RowHeadersVisible = false;
            guna2dgv_Items.Size = new Size(1468, 257);
            guna2dgv_Items.TabIndex = 10;
            guna2dgv_Items.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2dgv_Items.ThemeStyle.GridColor = Color.Black;
            guna2dgv_Items.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2dgv_Items.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Items.ThemeStyle.HeaderStyle.Height = 17;
            guna2dgv_Items.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_Items.ThemeStyle.RowsStyle.Height = 25;
            // 
            // SrNo
            // 
            SrNo.HeaderText = "S.No";
            SrNo.Name = "SrNo";
            // 
            // NIAPNum
            // 
            NIAPNum.HeaderText = "NIAP No.";
            NIAPNum.Name = "NIAPNum";
            // 
            // Description
            // 
            Description.HeaderText = "Description";
            Description.Name = "Description";
            // 
            // Quantity
            // 
            Quantity.HeaderText = "QTY";
            Quantity.Name = "Quantity";
            // 
            // Status
            // 
            Status.HeaderText = "Status";
            Status.Name = "Status";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(guna2dgv_BOQ);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 100);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1474, 258);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Data";
            // 
            // guna2dgv_BOQ
            // 
            dataGridViewCellStyle4.BackColor = Color.White;
            guna2dgv_BOQ.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            guna2dgv_BOQ.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            guna2dgv_BOQ.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            guna2dgv_BOQ.ColumnHeadersHeight = 17;
            guna2dgv_BOQ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2dgv_BOQ.Columns.AddRange(new DataGridViewColumn[] { Chk_Box, SNo, WorkOrder, MachineName, DesignNo, Stats, BOM, BOQ, UploadBy, DateTime, Custom, BOP });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            guna2dgv_BOQ.DefaultCellStyle = dataGridViewCellStyle6;
            guna2dgv_BOQ.Dock = DockStyle.Fill;
            guna2dgv_BOQ.GridColor = Color.Black;
            guna2dgv_BOQ.Location = new Point(3, 19);
            guna2dgv_BOQ.Name = "guna2dgv_BOQ";
            guna2dgv_BOQ.RowHeadersVisible = false;
            guna2dgv_BOQ.Size = new Size(1468, 236);
            guna2dgv_BOQ.TabIndex = 9;
            guna2dgv_BOQ.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2dgv_BOQ.ThemeStyle.GridColor = Color.Black;
            guna2dgv_BOQ.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2dgv_BOQ.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_BOQ.ThemeStyle.HeaderStyle.Height = 17;
            guna2dgv_BOQ.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2dgv_BOQ.ThemeStyle.RowsStyle.Height = 25;
            // 
            // Chk_Box
            // 
            Chk_Box.HeaderText = "";
            Chk_Box.Name = "Chk_Box";
            // 
            // SNo
            // 
            SNo.HeaderText = "SNo";
            SNo.Name = "SNo";
            // 
            // WorkOrder
            // 
            WorkOrder.HeaderText = "WO";
            WorkOrder.Name = "WorkOrder";
            // 
            // MachineName
            // 
            MachineName.HeaderText = "M/C Name";
            MachineName.Name = "MachineName";
            // 
            // DesignNo
            // 
            DesignNo.HeaderText = "Design No.";
            DesignNo.Name = "DesignNo";
            // 
            // Stats
            // 
            Stats.HeaderText = "Status";
            Stats.Name = "Stats";
            // 
            // BOM
            // 
            BOM.HeaderText = "BOM";
            BOM.Name = "BOM";
            // 
            // BOQ
            // 
            BOQ.HeaderText = "BOQ";
            BOQ.Name = "BOQ";
            // 
            // UploadBy
            // 
            UploadBy.HeaderText = "Upload By";
            UploadBy.Name = "UploadBy";
            // 
            // DateTime
            // 
            DateTime.HeaderText = "Date-Time";
            DateTime.Name = "DateTime";
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
            panel2.Controls.Add(rb_Released);
            panel2.Controls.Add(rb_Pending);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(cb_Type);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1474, 100);
            panel2.TabIndex = 4;
            // 
            // rb_Released
            // 
            rb_Released.AutoSize = true;
            rb_Released.Location = new Point(411, 34);
            rb_Released.Name = "rb_Released";
            rb_Released.Size = new Size(71, 19);
            rb_Released.TabIndex = 8;
            rb_Released.TabStop = true;
            rb_Released.Text = "Released";
            rb_Released.UseVisualStyleBackColor = true;
            // 
            // rb_Pending
            // 
            rb_Pending.AutoSize = true;
            rb_Pending.Location = new Point(295, 34);
            rb_Pending.Name = "rb_Pending";
            rb_Pending.Size = new Size(69, 19);
            rb_Pending.TabIndex = 7;
            rb_Pending.TabStop = true;
            rb_Pending.Text = "Pending";
            rb_Pending.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(237, 36);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 6;
            label2.Text = "Filter";
            // 
            // cb_Type
            // 
            cb_Type.FormattingEnabled = true;
            cb_Type.Items.AddRange(new object[] { "BOP", "Custom" });
            cb_Type.Location = new Point(70, 33);
            cb_Type.Name = "cb_Type";
            cb_Type.Size = new Size(121, 23);
            cb_Type.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 36);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 4;
            label1.Text = "Type";
            // 
            // BOQ_Authorise
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1474, 729);
            Controls.Add(panel3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(panel2);
            Name = "BOQ_Authorise";
            Text = "BOQ_Authorise";
            panel3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2dgv_Items).EndInit();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)guna2dgv_BOQ).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel3;
        private Button btn_Reject;
        private Button btn_Approve;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Panel panel2;
        private ComboBox cb_Type;
        private Label label1;
        private Label label2;
        private RadioButton rb_Released;
        private RadioButton rb_Pending;
        private Guna.UI2.WinForms.Guna2DataGridView guna2dgv_BOQ;
        private Guna.UI2.WinForms.Guna2DataGridView guna2dgv_Items;
        private DataGridViewTextBoxColumn SrNo;
        private DataGridViewTextBoxColumn NIAPNum;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewCheckBoxColumn Chk_Box;
        private DataGridViewTextBoxColumn SNo;
        private DataGridViewTextBoxColumn WorkOrder;
        private DataGridViewTextBoxColumn MachineName;
        private DataGridViewTextBoxColumn DesignNo;
        private DataGridViewTextBoxColumn Stats;
        private DataGridViewTextBoxColumn BOM;
        private DataGridViewTextBoxColumn BOQ;
        private DataGridViewTextBoxColumn UploadBy;
        private DataGridViewTextBoxColumn DateTime;
        private DataGridViewTextBoxColumn Custom;
        private DataGridViewTextBoxColumn BOP;
    }
}