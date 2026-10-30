namespace Practice_ProjM
{
    partial class Form1
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
            panel2 = new Panel();
            menuStrip1 = new MenuStrip();
            BOMUploaderToolStripMenuItem = new ToolStripMenuItem();
            BOMViewerToolStripMenuItem = new ToolStripMenuItem();
            BOMAssignmentToolStripMenuItem = new ToolStripMenuItem();
            BOQEditorToolStripMenuItem = new ToolStripMenuItem();
            authorizeReleaseToolStripMenuItem = new ToolStripMenuItem();
            releasesBOQToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(menuStrip1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1490, 799);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 31);
            panel2.Name = "panel2";
            panel2.Size = new Size(1490, 768);
            panel2.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.AppWorkspace;
            menuStrip1.Items.AddRange(new ToolStripItem[] { BOMUploaderToolStripMenuItem, BOMViewerToolStripMenuItem, BOMAssignmentToolStripMenuItem, BOQEditorToolStripMenuItem, authorizeReleaseToolStripMenuItem, releasesBOQToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1490, 31);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // BOMUploaderToolStripMenuItem
            // 
            BOMUploaderToolStripMenuItem.Font = new Font("Sitka Banner", 12F);
            BOMUploaderToolStripMenuItem.Name = "BOMUploaderToolStripMenuItem";
            BOMUploaderToolStripMenuItem.Size = new Size(114, 27);
            BOMUploaderToolStripMenuItem.Text = "BOM Uploader";
            BOMUploaderToolStripMenuItem.Click += BOMUploaderToolStripMenuItem_Click;
            // 
            // BOMViewerToolStripMenuItem
            // 
            BOMViewerToolStripMenuItem.Font = new Font("Sitka Banner", 12F);
            BOMViewerToolStripMenuItem.Name = "BOMViewerToolStripMenuItem";
            BOMViewerToolStripMenuItem.Size = new Size(101, 27);
            BOMViewerToolStripMenuItem.Text = "BOM Viewer";
            BOMViewerToolStripMenuItem.Click += BOMViewerToolStripMenuItem_Click;
            // 
            // BOMAssignmentToolStripMenuItem
            // 
            BOMAssignmentToolStripMenuItem.Font = new Font("Sitka Banner", 12F);
            BOMAssignmentToolStripMenuItem.Name = "BOMAssignmentToolStripMenuItem";
            BOMAssignmentToolStripMenuItem.Size = new Size(129, 27);
            BOMAssignmentToolStripMenuItem.Text = "BOM Assignment";
            BOMAssignmentToolStripMenuItem.Click += BOMAssignmentToolStripMenuItem_Click;
            // 
            // BOQEditorToolStripMenuItem
            // 
            BOQEditorToolStripMenuItem.Font = new Font("Sitka Banner", 12F);
            BOQEditorToolStripMenuItem.Name = "BOQEditorToolStripMenuItem";
            BOQEditorToolStripMenuItem.Size = new Size(93, 27);
            BOQEditorToolStripMenuItem.Text = "BOQ Editor";
            BOQEditorToolStripMenuItem.Click += BOQEditorToolStripMenuItem_Click;
            // 
            // authorizeReleaseToolStripMenuItem
            // 
            authorizeReleaseToolStripMenuItem.Font = new Font("Sitka Banner", 12F);
            authorizeReleaseToolStripMenuItem.Name = "authorizeReleaseToolStripMenuItem";
            authorizeReleaseToolStripMenuItem.Size = new Size(133, 27);
            authorizeReleaseToolStripMenuItem.Text = "Authorize Release";
            authorizeReleaseToolStripMenuItem.Click += authorizeReleaseToolStripMenuItem_Click;
            // 
            // releasesBOQToolStripMenuItem
            // 
            releasesBOQToolStripMenuItem.Font = new Font("Sitka Banner", 12F);
            releasesBOQToolStripMenuItem.Name = "releasesBOQToolStripMenuItem";
            releasesBOQToolStripMenuItem.Size = new Size(108, 27);
            releasesBOQToolStripMenuItem.Text = "Releases BOQ";
            releasesBOQToolStripMenuItem.Click += releasesBOQToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1490, 799);
            Controls.Add(panel1);
            MainMenuStrip = menuStrip1;
            MaximumSize = new Size(1587, 843);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem BOMUploaderToolStripMenuItem;
        private ToolStripMenuItem BOMViewerToolStripMenuItem;
        private ToolStripMenuItem BOMAssignmentToolStripMenuItem;
        private ToolStripMenuItem BOQEditorToolStripMenuItem;
        private ToolStripMenuItem authorizeReleaseToolStripMenuItem;
        private ToolStripMenuItem releasesBOQToolStripMenuItem;
    }
}