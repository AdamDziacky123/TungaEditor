using System;
using System.Windows.Forms;

namespace Absolventska
{
    public partial class FirstUserControl : UserControl
    {
        UCManager manager = UCManager.GetInstance();

        public FirstUserControl()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            manager.ControlToFront(1);
            manager.SetPanels(1);
        }

    }
}
