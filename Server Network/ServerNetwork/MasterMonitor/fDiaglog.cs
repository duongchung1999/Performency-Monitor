using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMonitor
{
    public partial class fDiaglog : Form
    {
        string content;
        public fDiaglog(string content)
        {
            InitializeComponent();
            this.content = content;
        }


        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void fDiaglog_Load(object sender, EventArgs e)
        {
            label1.Text = content;
            btnYes.Focus();
        }


        private void btnYes_KeyDown(object sender, KeyEventArgs e)
        {
            btnYes_Click(null, null);
        }
    }
}
