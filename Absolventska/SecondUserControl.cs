using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Absolventska // sadni si, zober papier do ruky, popremyslaj a tak rob.
{
    public partial class SecondUserControl : UserControl
    {
        #region Variables
        Dictionary<int, string> paths = new Dictionary<int, string>();
        Dictionary<int, string> words = new Dictionary<int, string>();
        Dictionary<string, string> output = new Dictionary<string, string>();
        List<Button> browseBTNs = new List<Button>();
        List<PictureBox> PBs = new List<PictureBox>();
        List<TextBox> TBs = new List<TextBox>();
        string filePath = string.Empty;
        int index = 0;
        // bool textFilled = false;
        bool alreadyUsed;
        #endregion

        public SecondUserControl()
        {
            InitializeComponent();
            PBsToList();
            ButtonsToList();
            TBsToList();
        }

        private void SecondUserControl_Load(object sender, EventArgs e) { }

        private void BrowseBTNFunction(Button btn)
        {
            //MessageBox.Show("Browse function.");
            bool added = false;

            using (OpenFileDialog OFD = new OpenFileDialog())
            {
                OFD.Filter = "JPG files (.jpg)|*.jpg";

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    index = browseBTNs.IndexOf(btn);
                    filePath = OFD.FileName; // check if index is not used. if yes, then rewrite

                    try
                    {
                        if (!paths.ContainsKey(index) && !paths.ContainsValue(filePath)) paths.Add(index, filePath); //wrong paths [] =
                        else MessageBox.Show("Image was already used.");

                        if (TBs[index].Text.Length > 0 && paths.ContainsKey(index)) PBs[index].Visible = true;
                    }

                    catch (System.ArgumentException e)
                    {
                        MessageBox.Show("Image already assigned. Please use the Reset button.");
                    }

                    added = true;
                }
            }
        }

        #region ToListFunctions 
        private void ButtonsToList()
        {
            browseBTNs.Add(button3);
            browseBTNs.Add(button4);
            browseBTNs.Add(button5);
            browseBTNs.Add(button6);
            browseBTNs.Add(button7);
            browseBTNs.Add(button8);
            browseBTNs.Add(button9);
            browseBTNs.Add(button10);
            browseBTNs.Add(button11);
            browseBTNs.Add(button12);
        }

        private void PBsToList()
        {
            PBs.Add(pictureBox1);
            PBs.Add(pictureBox2);
            PBs.Add(pictureBox3);
            PBs.Add(pictureBox4);
            PBs.Add(pictureBox5);
            PBs.Add(pictureBox6);
            PBs.Add(pictureBox7);
            PBs.Add(pictureBox8);
            PBs.Add(pictureBox9);
            PBs.Add(pictureBox10);
        }

        private void TBsToList()
        {
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
        }
        #endregion

        private void Reset()
        {
            foreach (TextBox TB in TBs)
            {
                TB.Text = string.Empty;
            }

            foreach (PictureBox PB in PBs)
            {
                PB.Visible = false;
            }

            paths.Clear();
            index = 0;
        }

        private void WriteWords()
        {
            int errorIndex;
            alreadyUsed = false;

            try 
            {
                /*if (!words.ContainsKey(TBs.IndexOf(textBox1)) && !words.ContainsValue(textBox1.Text)) { index = TBs.IndexOf(textBox1); words.Add(index, textBox1.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox1);
                    textBox1.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox2)) && !words.ContainsValue(textBox2.Text)) { index = TBs.IndexOf(textBox2); words.Add(index, textBox2.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox2);
                    textBox2.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox3)) && !words.ContainsValue(textBox3.Text)) { index = TBs.IndexOf(textBox3); words.Add(index, textBox3.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox3);
                    textBox3.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox4)) && !words.ContainsValue(textBox4.Text)) { index = TBs.IndexOf(textBox4); words.Add(index, textBox4.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox4);
                    textBox4.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox5)) && !words.ContainsValue(textBox5.Text)) { index = TBs.IndexOf(textBox5); words.Add(index, textBox5.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox5);
                    textBox5.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox6)) && !words.ContainsValue(textBox6.Text)) { index = TBs.IndexOf(textBox6); words.Add(index, textBox6.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox6);
                    textBox6.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox7)) && !words.ContainsValue(textBox7.Text)) { index = TBs.IndexOf(textBox7); words.Add(index, textBox7.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox7);
                    textBox7.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox8)) && !words.ContainsValue(textBox8.Text)) { index = TBs.IndexOf(textBox8); words.Add(index, textBox8.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox8);
                    textBox8.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox9)) && !words.ContainsValue(textBox9.Text)) { index = TBs.IndexOf(textBox9); words.Add(index, textBox9.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox9);
                    textBox9.Text = string.Empty;
                }

                if (!words.ContainsKey(TBs.IndexOf(textBox10)) && !words.ContainsValue(textBox10.Text)) { index = TBs.IndexOf(textBox10); words.Add(index, textBox10.Text); }
                else
                {
                    //alreadyUsed = true; errorIndex = TBs.IndexOf(textBox10);
                    textBox10.Text = string.Empty;
                }*/

                for (int i = 0; i < TBs.Count; i++)
                {
                    while (words.ContainsKey(i) && words.ContainsValue(TBs[i].Text))
                    {
                        MessageBox.Show("Word already used. Type in another word.");
                        TBs[i].Text = string.Empty;
                        //words.Remove(i);
                        //break;
                    }
                    words.Add(i, TBs[i].Text);

                   /* if (!words.ContainsKey(i) && !words.ContainsValue(TBs[i].Text)) words.Add(i, TBs[i].Text);
                    else
                    {
                        MessageBox.Show("Word already used. Type in another word.");
                        TBs[i].Text = string.Empty;
                        break;
                    }*/
                }
            }

            catch (System.ArgumentException e)
            {
                MessageBox.Show("Word already used.");
            }
        }

        #region Buttons

        private void button3_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            BrowseBTNFunction(button12);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        #endregion

        #region textBox_TextChanged 

        private void textBox1_TextChanged(object sender, EventArgs e) //adding to dictionary too ineffective, would be better in confirm button
        {
            index = TBs.IndexOf(textBox1);
            //Check(textBox1);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox1.Text)) words.Add(index, textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox2);
            //Check(textBox2);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox2.Text)) words.Add(index, textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox3);
            //Check(textBox3);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox3.Text)) words.Add(index, textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox4);
            //Check(textBox4);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox4.Text)) words.Add(index, textBox4.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox5);
            //Check(textBox5);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox5.Text)) words.Add(index, textBox5.Text);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox6);
            //Check(textBox6);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox6.Text)) words.Add(index, textBox6.Text);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox7);
            //Check(textBox7);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox7.Text)) words.Add(index, textBox7.Text);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox8);
            //Check(textBox8);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox8.Text)) words.Add(index, textBox8.Text);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox9);
            //Check(textBox9);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox9.Text)) words.Add(index, textBox9.Text);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            index = TBs.IndexOf(textBox10);
            //Check(textBox10);
            if (paths.ContainsKey(index)) PBs[index].Visible = true;
            //if (!words.ContainsKey(index) && !paths.ContainsValue(textBox10.Text)) words.Add(index, textBox10.Text);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            WriteWords();

            if (words.Count == 10 && paths.Count == 10)
            {
                for (int i = 0; i < TBs.Count; i++)
                {
                    output.Add(words[i], paths[i]);
                }
            }

            else MessageBox.Show("Missing words or pictures.");
        }
    }
}
