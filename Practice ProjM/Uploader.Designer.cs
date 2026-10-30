namespace Practice_ProjM
{
    partial class Uploader
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
            panel1 = new Panel();
            panel7 = new Panel();
            groupBox3 = new GroupBox();
            dgv_CustomisedItem = new DataGridView();
            Customized_Serial_No = new DataGridViewTextBoxColumn();
            Customized_Description = new DataGridViewTextBoxColumn();
            Customised_QTY = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            dgv_BOPItem = new DataGridView();
            Serial = new DataGridViewTextBoxColumn();
            BOPDescription = new DataGridViewTextBoxColumn();
            BOP_QTY = new DataGridViewTextBoxColumn();
            panel6 = new Panel();
            groupBox1 = new GroupBox();
            dgv_MPL = new DataGridView();
            panel5 = new Panel();
            btn_Save = new Button();
            btn_Note = new Button();
            btn_Edit = new Button();
            btn_Remove = new Button();
            btn_Add = new Button();
            panel4 = new Panel();
            label9 = new Label();
            btn_Download = new Button();
            btn_Upload = new Button();
            btn_Browse = new Button();
            txt_Path = new TextBox();
            panel3 = new Panel();
            label11 = new Label();
            txt_NMT_SerialNo = new TextBox();
            label10 = new Label();
            txt_PartNo = new TextBox();
            label8 = new Label();
            txt_Rev = new TextBox();
            label7 = new Label();
            txt_Assy = new TextBox();
            label6 = new Label();
            txt_Machine = new TextBox();
            label5 = new Label();
            txt_Design = new TextBox();
            panel2 = new Panel();
            txt_Customer = new TextBox();
            label12 = new Label();
            txt_QTY = new TextBox();
            label4 = new Label();
            txt_Description = new TextBox();
            label3 = new Label();
            label2 = new Label();
            cb_Type = new ComboBox();
            txt_IDKey = new TextBox();
            label1 = new Label();
            MPL_Serial_No = new DataGridViewTextBoxColumn();
            NMT_PartNum = new DataGridViewTextBoxColumn();
            PartNo = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            Type = new DataGridViewTextBoxColumn();
            QTY = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel7.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_CustomisedItem).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_BOPItem).BeginInit();
            panel6.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_MPL).BeginInit();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1474, 729);
            panel1.TabIndex = 0;
            // 
            // panel7
            // 
            panel7.Controls.Add(groupBox3);
            panel7.Controls.Add(groupBox2);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(0, 489);
            panel7.Name = "panel7";
            panel7.Size = new Size(1474, 240);
            panel7.TabIndex = 5;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgv_CustomisedItem);
            groupBox3.Dock = DockStyle.Right;
            groupBox3.Location = new Point(721, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(753, 240);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Customised Item";
            // 
            // dgv_CustomisedItem
            // 
            dgv_CustomisedItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_CustomisedItem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_CustomisedItem.Columns.AddRange(new DataGridViewColumn[] { Customized_Serial_No, Customized_Description, Customised_QTY });
            dgv_CustomisedItem.Dock = DockStyle.Fill;
            dgv_CustomisedItem.Location = new Point(3, 19);
            dgv_CustomisedItem.Name = "dgv_CustomisedItem";
            dgv_CustomisedItem.Size = new Size(747, 218);
            dgv_CustomisedItem.TabIndex = 0;
            // 
            // Customized_Serial_No
            // 
            Customized_Serial_No.HeaderText = "SR. No.";
            Customized_Serial_No.Name = "Customized_Serial_No";
            Customized_Serial_No.ReadOnly = true;
            // 
            // Customized_Description
            // 
            Customized_Description.HeaderText = "Description";
            Customized_Description.Name = "Customized_Description";
            Customized_Description.ReadOnly = true;
            // 
            // Customised_QTY
            // 
            Customised_QTY.HeaderText = "QTY";
            Customised_QTY.Name = "Customised_QTY";
            Customised_QTY.ReadOnly = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgv_BOPItem);
            groupBox2.Dock = DockStyle.Left;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(721, 240);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "BOP Item";
            // 
            // dgv_BOPItem
            // 
            dgv_BOPItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_BOPItem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_BOPItem.Columns.AddRange(new DataGridViewColumn[] { Serial, BOPDescription, BOP_QTY });
            dgv_BOPItem.Dock = DockStyle.Fill;
            dgv_BOPItem.Location = new Point(3, 19);
            dgv_BOPItem.Name = "dgv_BOPItem";
            dgv_BOPItem.Size = new Size(715, 218);
            dgv_BOPItem.TabIndex = 0;
            // 
            // Serial
            // 
            Serial.HeaderText = "SR. No.";
            Serial.Name = "Serial";
            Serial.ReadOnly = true;
            // 
            // BOPDescription
            // 
            BOPDescription.HeaderText = "Description";
            BOPDescription.Name = "BOPDescription";
            BOPDescription.ReadOnly = true;
            // 
            // BOP_QTY
            // 
            BOP_QTY.HeaderText = "QTY";
            BOP_QTY.Name = "BOP_QTY";
            BOP_QTY.ReadOnly = true;
            // 
            // panel6
            // 
            panel6.Controls.Add(groupBox1);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 180);
            panel6.Name = "panel6";
            panel6.Size = new Size(1474, 309);
            panel6.TabIndex = 4;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgv_MPL);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1474, 309);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "MPL";
            // 
            // dgv_MPL
            // 
            dgv_MPL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_MPL.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_MPL.Columns.AddRange(new DataGridViewColumn[] { MPL_Serial_No, NMT_PartNum, PartNo, Description, Type, QTY });
            dgv_MPL.Dock = DockStyle.Fill;
            dgv_MPL.Location = new Point(3, 19);
            dgv_MPL.Name = "dgv_MPL";
            dgv_MPL.Size = new Size(1468, 287);
            dgv_MPL.TabIndex = 0;
            dgv_MPL.CellClick += dgv_MPL_CellClick;
            // 
            // panel5
            // 
            panel5.Controls.Add(btn_Save);
            panel5.Controls.Add(btn_Note);
            panel5.Controls.Add(btn_Edit);
            panel5.Controls.Add(btn_Remove);
            panel5.Controls.Add(btn_Add);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 150);
            panel5.Name = "panel5";
            panel5.Size = new Size(1474, 30);
            panel5.TabIndex = 3;
            // 
            // btn_Save
            // 
            btn_Save.Location = new Point(359, 4);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(75, 23);
            btn_Save.TabIndex = 16;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += btn_Save_Click;
            // 
            // btn_Note
            // 
            btn_Note.Location = new Point(255, 4);
            btn_Note.Name = "btn_Note";
            btn_Note.Size = new Size(75, 23);
            btn_Note.TabIndex = 15;
            btn_Note.Text = "Note";
            btn_Note.UseVisualStyleBackColor = true;
            btn_Note.Click += btn_Note_Click;
            // 
            // btn_Edit
            // 
            btn_Edit.Location = new Point(174, 4);
            btn_Edit.Name = "btn_Edit";
            btn_Edit.Size = new Size(75, 23);
            btn_Edit.TabIndex = 14;
            btn_Edit.Text = "Edit";
            btn_Edit.UseVisualStyleBackColor = true;
            btn_Edit.Click += btn_Edit_Click;
            // 
            // btn_Remove
            // 
            btn_Remove.ForeColor = Color.Red;
            btn_Remove.Location = new Point(93, 4);
            btn_Remove.Name = "btn_Remove";
            btn_Remove.Size = new Size(75, 23);
            btn_Remove.TabIndex = 13;
            btn_Remove.Text = "- Remove";
            btn_Remove.UseVisualStyleBackColor = true;
            btn_Remove.Click += btn_Remove_Click;
            // 
            // btn_Add
            // 
            btn_Add.Location = new Point(12, 4);
            btn_Add.Name = "btn_Add";
            btn_Add.Size = new Size(75, 23);
            btn_Add.TabIndex = 12;
            btn_Add.Text = "+ Add";
            btn_Add.UseVisualStyleBackColor = true;
            btn_Add.Click += btn_Add_Click;
            // 
            // panel4
            // 
            panel4.Controls.Add(label9);
            panel4.Controls.Add(btn_Download);
            panel4.Controls.Add(btn_Upload);
            panel4.Controls.Add(btn_Browse);
            panel4.Controls.Add(txt_Path);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 100);
            panel4.Name = "panel4";
            panel4.Size = new Size(1474, 50);
            panel4.TabIndex = 2;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(18, 18);
            label9.Name = "label9";
            label9.Size = new Size(31, 15);
            label9.TabIndex = 8;
            label9.Text = "Path";
            // 
            // btn_Download
            // 
            btn_Download.Location = new Point(629, 14);
            btn_Download.Name = "btn_Download";
            btn_Download.Size = new Size(89, 23);
            btn_Download.TabIndex = 11;
            btn_Download.Text = "↓ Download";
            btn_Download.UseVisualStyleBackColor = true;
            // 
            // btn_Upload
            // 
            btn_Upload.Location = new Point(548, 14);
            btn_Upload.Name = "btn_Upload";
            btn_Upload.Size = new Size(75, 23);
            btn_Upload.TabIndex = 10;
            btn_Upload.Text = "↑ Upload";
            btn_Upload.UseVisualStyleBackColor = true;
            // 
            // btn_Browse
            // 
            btn_Browse.Location = new Point(467, 14);
            btn_Browse.Name = "btn_Browse";
            btn_Browse.Size = new Size(75, 23);
            btn_Browse.TabIndex = 9;
            btn_Browse.Text = "Browse";
            btn_Browse.UseVisualStyleBackColor = true;
            // 
            // txt_Path
            // 
            txt_Path.Location = new Point(55, 14);
            txt_Path.Name = "txt_Path";
            txt_Path.Size = new Size(406, 23);
            txt_Path.TabIndex = 7;
            // 
            // panel3
            // 
            panel3.Controls.Add(label11);
            panel3.Controls.Add(txt_NMT_SerialNo);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(txt_PartNo);
            panel3.Controls.Add(label8);
            panel3.Controls.Add(txt_Rev);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(txt_Assy);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(txt_Machine);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(txt_Design);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 50);
            panel3.Name = "panel3";
            panel3.Size = new Size(1474, 50);
            panel3.TabIndex = 1;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(1161, 16);
            label11.Name = "label11";
            label11.Size = new Size(103, 15);
            label11.TabIndex = 14;
            label11.Text = "NMT Part number";
            // 
            // txt_NMT_SerialNo
            // 
            txt_NMT_SerialNo.Location = new Point(1269, 13);
            txt_NMT_SerialNo.Name = "txt_NMT_SerialNo";
            txt_NMT_SerialNo.Size = new Size(181, 23);
            txt_NMT_SerialNo.TabIndex = 13;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(971, 16);
            label10.Name = "label10";
            label10.Size = new Size(50, 15);
            label10.TabIndex = 12;
            label10.Text = "Part No.";
            // 
            // txt_PartNo
            // 
            txt_PartNo.Location = new Point(1024, 13);
            txt_PartNo.Name = "txt_PartNo";
            txt_PartNo.Size = new Size(119, 23);
            txt_PartNo.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(812, 16);
            label8.Name = "label8";
            label8.Size = new Size(26, 15);
            label8.TabIndex = 10;
            label8.Text = "Rev";
            // 
            // txt_Rev
            // 
            txt_Rev.Location = new Point(853, 13);
            txt_Rev.Name = "txt_Rev";
            txt_Rev.Size = new Size(102, 23);
            txt_Rev.TabIndex = 9;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(617, 16);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 8;
            label7.Text = "Assy";
            // 
            // txt_Assy
            // 
            txt_Assy.Location = new Point(654, 13);
            txt_Assy.Name = "txt_Assy";
            txt_Assy.Size = new Size(111, 23);
            txt_Assy.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(253, 16);
            label6.Name = "label6";
            label6.Size = new Size(53, 15);
            label6.TabIndex = 6;
            label6.Text = "Machine";
            // 
            // txt_Machine
            // 
            txt_Machine.Location = new Point(312, 13);
            txt_Machine.Name = "txt_Machine";
            txt_Machine.Size = new Size(257, 23);
            txt_Machine.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(13, 16);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 4;
            label5.Text = "Design";
            // 
            // txt_Design
            // 
            txt_Design.Location = new Point(62, 13);
            txt_Design.Name = "txt_Design";
            txt_Design.Size = new Size(173, 23);
            txt_Design.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(txt_Customer);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(txt_QTY);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txt_Description);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(cb_Type);
            panel2.Controls.Add(txt_IDKey);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1474, 50);
            panel2.TabIndex = 0;
            // 
            // txt_Customer
            // 
            txt_Customer.Location = new Point(555, 11);
            txt_Customer.Name = "txt_Customer";
            txt_Customer.Size = new Size(111, 23);
            txt_Customer.TabIndex = 13;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(1199, 17);
            label12.Name = "label12";
            label12.Size = new Size(29, 15);
            label12.TabIndex = 12;
            label12.Text = "QTY";
            // 
            // txt_QTY
            // 
            txt_QTY.Location = new Point(1240, 14);
            txt_QTY.Name = "txt_QTY";
            txt_QTY.Size = new Size(181, 23);
            txt_QTY.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(724, 14);
            label4.Name = "label4";
            label4.Size = new Size(67, 15);
            label4.TabIndex = 6;
            label4.Text = "Description";
            // 
            // txt_Description
            // 
            txt_Description.Location = new Point(797, 11);
            txt_Description.Name = "txt_Description";
            txt_Description.Size = new Size(358, 23);
            txt_Description.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(490, 14);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 3;
            label3.Text = "Customer";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(237, 14);
            label2.Name = "label2";
            label2.Size = new Size(99, 15);
            label2.TabIndex = 2;
            label2.Text = "Identification Key";
            // 
            // cb_Type
            // 
            cb_Type.FormattingEnabled = true;
            cb_Type.Items.AddRange(new object[] { "BOP", "Customised" });
            cb_Type.Location = new Point(76, 11);
            cb_Type.Name = "cb_Type";
            cb_Type.Size = new Size(121, 23);
            cb_Type.TabIndex = 1;
            // 
            // txt_IDKey
            // 
            txt_IDKey.Location = new Point(359, 11);
            txt_IDKey.Name = "txt_IDKey";
            txt_IDKey.Size = new Size(100, 23);
            txt_IDKey.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 14);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "BOM Type";
            // 
            // MPL_Serial_No
            // 
            MPL_Serial_No.HeaderText = "S. No.";
            MPL_Serial_No.Name = "MPL_Serial_No";
            MPL_Serial_No.ReadOnly = true;
            // 
            // NMT_PartNum
            // 
            NMT_PartNum.HeaderText = "NMT Part Number";
            NMT_PartNum.Name = "NMT_PartNum";
            // 
            // PartNo
            // 
            PartNo.HeaderText = "Part No.";
            PartNo.Name = "PartNo";
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
            QTY.ReadOnly = true;
            // 
            // Uploader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1474, 729);
            Controls.Add(panel1);
            Name = "Uploader";
            Text = "Uploader";
            panel1.ResumeLayout(false);
            panel7.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_CustomisedItem).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_BOPItem).EndInit();
            panel6.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_MPL).EndInit();
            panel5.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private Panel panel7;
        private TextBox txt_IDKey;
        private Label label1;
        private Label label3;
        private Label label2;
        private ComboBox cb_Type;
        private Label label8;
        private TextBox txt_Rev;
        private Label label7;
        private TextBox txt_Assy;
        private Label label6;
        private TextBox txt_Machine;
        private Label label5;
        private TextBox txt_Design;
        private Label label9;
        private TextBox txt_Path;
        private Label label4;
        private TextBox txt_Description;
        private Button btn_Download;
        private Button btn_Upload;
        private Button btn_Browse;
        private Button btn_Edit;
        private Button btn_Remove;
        private Button btn_Add;
        private Button btn_Save;
        private Button btn_Note;
        private GroupBox groupBox3;
        private DataGridView dgv_CustomisedItem;
        private GroupBox groupBox2;
        private DataGridView dgv_BOPItem;
        private GroupBox groupBox1;
        private DataGridView dgv_MPL;
        private Label label11;
        private TextBox txt_NMT_SerialNo;
        private Label label10;
        private TextBox txt_PartNo;
        private DataGridViewTextBoxColumn Serial;
        private DataGridViewTextBoxColumn BOPDescription;
        private DataGridViewTextBoxColumn BOP_QTY;
        private DataGridViewTextBoxColumn Customized_Serial_No;
        private DataGridViewTextBoxColumn Customized_Description;
        private DataGridViewTextBoxColumn Customised_QTY;
        private Label label12;
        private TextBox txt_QTY;
        private TextBox txt_Customer;
        private DataGridViewTextBoxColumn MPL_Serial_No;
        private DataGridViewTextBoxColumn NMT_PartNum;
        private DataGridViewTextBoxColumn PartNo;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn Type;
        private DataGridViewTextBoxColumn QTY;
    }
}