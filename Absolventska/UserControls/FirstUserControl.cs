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
