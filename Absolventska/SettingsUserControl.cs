using System;
using System.Linq;
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
                manager.SetPath_words(textBox1.Text + @"\Words.txt");
                MessageBox.Show("Path changed successfully.");
            }
            else MessageBox.Show("Please, insert a path.");
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            textBox1.Text = Environment.CurrentDirectory + @"\Tunga Files";

            if (textBox1.Text.Count() >= 0)
            {
                manager.SetPath_files(textBox1.Text);
                manager.SetPath_words(textBox1.Text + @"\Words.txt");
                MessageBox.Show("Path set to default.");
            }

            else MessageBox.Show("Please, insert a path.");
        }
    }
}
