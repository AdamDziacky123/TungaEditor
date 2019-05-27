using System;
using System.Linq;
using System.Windows.Forms;

namespace Absolventska
{
    public partial class SettingsUserControl : UserControl
    {
        UCManager manager = UCManager.GetInstance();
        SerializationClass serialization = SerializationClass.GetInstance();

        public SettingsUserControl()
        {
            InitializeComponent();
            textBox1.Text = manager.GetPath_files();
        }

        #region btn_Click

        private void btnChangePath_Click(object sender, EventArgs e)
        {
            MessageBox.Show("To function properly, be sure this path is the same as application path. Otherwise, some problems might occur.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
            if (textBox1.Text.Count() > 0 && textBox1.Text != manager.GetPath_files())
            {
                manager.SetPath_files(textBox1.Text, false);
                manager.SetPath_words(textBox1.Text + @"\Words.txt");
                MessageBox.Show("Path changed successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                serialization.AddPathToReg();
            }

            else MessageBox.Show("Please, insert a new path.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        } //apply changes

        private void btnDefault_Click(object sender, EventArgs e) // setting default path + adding to register
        {
            textBox1.Text = Environment.CurrentDirectory + @"\Tunga Files";

            if (textBox1.Text.Count() >= 0)
            {
                manager.SetPath_files(textBox1.Text, false);
                manager.SetPath_words(textBox1.Text + @"\Words.txt");
                MessageBox.Show("Path set to default.");
                serialization.AddPathToReg();
            }

            else MessageBox.Show("Please, insert a path.");
        }

        #endregion
    }
}
