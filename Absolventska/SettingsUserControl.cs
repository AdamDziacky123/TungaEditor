using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Absolventska
{
    public partial class SettingsUserControl : UserControl
    {
        UCManager manager = UCManager.GetInstance();

        public SettingsUserControl()
        {
            InitializeComponent();
            textBox1.Text = manager.GetPath_files();
        }

        private void btnChangePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog OFD = new FolderBrowserDialog())
            {
                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = OFD.SelectedPath;
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Count() >= 0)
            {
                manager.SetPath_files(textBox1.Text);
                MessageBox.Show("Path changed successfully.");
            }
            else MessageBox.Show("Please, insert a path.");
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textBox1.Text = manager.GetPath_files();
        }
    }
}
