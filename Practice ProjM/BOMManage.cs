using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_ProjM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void LoadFormIntoPanel(Form form)
        {
            this.panel2.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(form);
            this.panel2.Tag = form;
            form.Show();
        }
        private void BOMUploaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Uploader();
            LoadFormIntoPanel(form);
        }

        private void BOMViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Viewer();
            LoadFormIntoPanel(form);
        }

        private void BOMAssignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Assignment();
            LoadFormIntoPanel(form);
        }

        private void BOQEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new BOQ_Editor();
            LoadFormIntoPanel(form);
        }

        private void authorizeReleaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new BOQ_Authorise();
            LoadFormIntoPanel(form);
        }

        private void releasesBOQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new Released_BOQ();
            LoadFormIntoPanel(form);
        }
    }
}
