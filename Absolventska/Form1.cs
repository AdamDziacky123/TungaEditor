using System;
using System.Windows.Forms;
using System.Diagnostics;
//using System.Windows.Forms.Screen;

namespace Absolventska
{
    public partial class Form1 : Form //// find out the resolution, create a variable in manager, set UC position according to that. Variable is set up according to current resolution. 
    {
        UCManager manager = UCManager.GetInstance();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            manager.Initiate(this);
            manager.ControlToFront(0);
            manager.SetPanels(0);
            SerializationClass.GetInstance().AddPathToReg();
        }

        #region btn_Click

        private void btnExit_Click(object sender, EventArgs e)
        {
            manager.SetPath_files(null,true);
            SerializationClass.GetInstance().AddPathToReg();
            Application.Exit();
        }

        private void btnWelcome_Click(object sender, EventArgs e)
        {
            manager.ControlToFront(0);
            manager.SetPanels(0);
        }

        private void btnPictures_Click(object sender, EventArgs e)
        {
            manager.ControlToFront(1);
            manager.SetPanels(1);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            manager.ControlToFront(2);
            manager.SetPanels(2);
        }

        private void btnFB_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/adam.dziacky");
        }

        private void btnLinkedIn_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.linkedin.com/in/adam-dziacky-8ba901156");
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; // push
        }

        private void btnMaximize_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized) this.WindowState = FormWindowState.Maximized;
            else this.WindowState = FormWindowState.Normal;
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("adam.dziacky@studentstc.sk");
            MessageBox.Show("Mail adress copied to clipboard.");
        }

        #endregion

    }
}
