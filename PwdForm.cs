using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CobaltWinTracker
{
    public partial class PwdForm : Form
    {
        public PwdForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            string pwd = PwdBox.Text;
            const string hash = "$2a$12$XA8qFogc2C6P50cubgZeN.qnTwPx7qJsaTZ/pr0ll4YFPJdTIw/Bm";

            PwdBox.Text = "";

            if (BCrypt.CheckPassword(pwd, hash))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.None;
            }

            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            PwdBox.Text = "";

            Close();
        }

        private void PwdForm_Shown(object sender, EventArgs e)
        {
            PwdBox.Text = "";
        }
    }
}
