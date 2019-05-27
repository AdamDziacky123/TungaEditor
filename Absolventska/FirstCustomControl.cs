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
    public partial class FirstCustomControl : UserControl
    {
        public FirstCustomControl()
        {
            InitializeComponent();
        }

        private void FirstCustomControl_Load(object sender, EventArgs e)
        {
            
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool allFilled = true;
            List<TextBox> TBs = new List<TextBox>();

            TBs.Add(textBox1);
            TBs.Add(textBox2);
            TBs.Add(textBox3);
            TBs.Add(textBox4);
            TBs.Add(textBox5);
            TBs.Add(textBox6);
            TBs.Add(textBox7);
            TBs.Add(textBox8);
            TBs.Add(textBox9);
            TBs.Add(textBox10);

            foreach (TextBox tb in TBs)
            {
                if (tb.Text.Length == 0)
                {
                    allFilled = false;
                }
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
