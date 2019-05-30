using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Absolventska 
{
    public partial class SecondUserControl : UserControl
    {
        #region Variables

        protected UCManager manager = UCManager.GetInstance();
        SerializationClass serialization = SerializationClass.GetInstance();

        public List<Button> browseBTNs; //SUC browse btns
        public List<PictureBox> PBs; //SUC pictureboxes
        public List<TextBox> TBs; // SUC textboxes

        protected Dictionary<int, string> paths; //index + path
        protected Dictionary<int, string> words; // index + words
        protected Dictionary<string, string> output; // words + paths

        public static int numOfWords;
        int index = 0;

        #endregion

        #region InitiateFunctions 

        private void Initiate()
        {
            browseBTNs = serialization.browseBTNs;
            TBs = serialization.TBs;
            PBs = serialization.PBs;
            paths = serialization.paths;
            words = serialization.words;
            output = serialization.output;

            ButtonsToList(); //just adding original objects do lists - in case that teacher doesnt want to change the number
            PBsToList();
            TBsToList();

            AssignFunctions();
        }

        private void ButtonsToList() //default case - 5 rows
        {
            for (int i = 1; i < 11; i++)
            {
                browseBTNs.Add(Controls.Find(string.Format("button{0}", i), true).FirstOrDefault() as Button);
            }
        }

        private void PBsToList()
        {
            for (int i = 1; i < 11; i++)
            {
                PBs.Add(Controls.Find(string.Format("pictureBox{0}", i), true).FirstOrDefault() as PictureBox);
            }
        }

        private void TBsToList()
        {
            for (int i = 1; i < 11; i++)
            {
                TBs.Add(Controls.Find(string.Format("textBox{0}", i), true).FirstOrDefault() as TextBox);
            }
        }

        #endregion

        #region OtherMethods

        private void Reset(object sender, EventArgs e) //clears all textboxes and lists
        {
            foreach (TextBox TB in TBs)
            {
                TB.Text = string.Empty;
            }

            foreach (PictureBox PB in PBs)
            {
                PB.Visible = false;
            }

            if(paths != null || paths.Count > 0) paths.Clear();
            if (words != null || words.Count > 0) words.Clear();
            if (output != null || output.Count > 0) output.Clear();
            index = 0;
        } 

        protected void Reset() //using two types of same function - one for button as eventHandler 
        {
            foreach (TextBox TB in TBs)
            {
                TB.Text = string.Empty;
            }

            foreach (PictureBox PB in PBs)
            {
                PB.Visible = false;
            }

            if (paths != null || paths.Count > 0) paths.Clear();
            if (words != null || words.Count > 0) words.Clear();
            if (output != null || output.Count > 0) output.Clear();
            index = 0;
        }

        private void AssignFunctions()
        {
            foreach (Button btn in browseBTNs) // assigning browse button functions
            {
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += new EventHandler(OnClick);
            }

            foreach (TextBox TB in TBs)
            {
                TB.TextChanged += new EventHandler(TbTextChanged);
            }

            btnReset.Click += new EventHandler(Reset);
        }

        private void AddControlsToLists()
        {
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                browseBTNs.Add((Button)tableLayoutPanel1.Controls.Find(string.Format("BrowseBTN{0}{1}", 1, i), true).FirstOrDefault());
                browseBTNs.Add((Button)tableLayoutPanel1.Controls.Find(string.Format("BrowseBTN{0}{1}", 4, i), true).FirstOrDefault());
                PBs.Add((PictureBox)tableLayoutPanel1.Controls.Find(string.Format("PB{0}{1}", 2, i), true).FirstOrDefault());
                PBs.Add((PictureBox)tableLayoutPanel1.Controls.Find(string.Format("PB{0}{1}", 5, i), true).FirstOrDefault());
                TBs.Add((TextBox)tableLayoutPanel1.Controls.Find(string.Format("TB{0}{1}", 0, i), true).FirstOrDefault());
                TBs.Add((TextBox)tableLayoutPanel1.Controls.Find(string.Format("TB{0}{1}", 3, i), true).FirstOrDefault());
            }
        }

        private void BrowseBTNFunction(Button btn)
        {
            string filePath = string.Empty;

            using (OpenFileDialog OFD = new OpenFileDialog())
            {
                OFD.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"; //adding several file type options

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    index = browseBTNs.IndexOf(btn);
                    filePath = OFD.FileName;

                    try //trying, if word or picture was not already used.
                    {
                        if (!paths.ContainsKey(index) && !paths.ContainsValue(filePath)) paths.Add(index, filePath);
                        else
                        {
                            MessageBox.Show("Image was already used.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        if (TBs[index].Text.Length > 0 && paths.ContainsKey(index)) PBs[index].Visible = true;
                    }

                    catch (System.ArgumentException e)
                    {
                        MessageBox.Show("Image already assigned. Please use the Reset button.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            BrowseBTNFunction(btn);
        } //for Browse buttons

        private void TbTextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            index = TBs.IndexOf(tb);
            if (paths.ContainsKey(index) && tb.Text.Length > 0) PBs[index].Visible = true;
            else PBs[index].Visible = false;
        } //checking if it should show success icon

        private void BTNsetRows_Click(object sender, EventArgs e) // creating dynamic table + assigning objects + adding them to lists
        {
            //List<Button> BTNList = new List<Button>();
            Image image = TungaEditor.Properties.Resources.success; //Success image
            int tmp = (int)numericUpDown1.Value;

            PBs.Clear(); //clearing original lists
            TBs.Clear();
            browseBTNs.Clear();

            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear(); //clearing original column styles
            tableLayoutPanel1.RowStyles.Clear();

            tableLayoutPanel1.RowCount = tmp;

            for (int x = 0; x <= tableLayoutPanel1.RowCount; x++) // setting the heights of table rows
            {
                if (x <= tableLayoutPanel1.RowCount - 1) tableLayoutPanel1.RowStyles.Add(new RowStyle() { Height = (tableLayoutPanel1.Height / tableLayoutPanel1.RowCount) - 1, SizeType = SizeType.Absolute });
                else tableLayoutPanel1.RowStyles.Add(new RowStyle() { Height = tableLayoutPanel1.Height / tableLayoutPanel1.RowCount, SizeType = SizeType.Absolute });
                //MessageBox.Show(tableLayoutPanel1.RowStyles[x].Height.ToString());
            }

            tableLayoutPanel1.ColumnCount = 6; // setting widths of columns manually
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle() { Width = 50, SizeType = SizeType.Percent });
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle() { Width = 25, SizeType = SizeType.Percent });
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle() { Width = 15, SizeType = SizeType.Percent });
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle() { Width = 50, SizeType = SizeType.Percent });
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle() { Width = 25, SizeType = SizeType.Percent });
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle() { Width = 15, SizeType = SizeType.Percent });

            for (int i = 0; i < tableLayoutPanel1.RowCount; i++) //adding all objects into table + adding objects to lists
            {
                tableLayoutPanel1.Controls.Add(new TextBox //object parametres
                {
                    TextAlign = HorizontalAlignment.Center,
                    BackColor = Color.FromArgb(36, 46, 64),
                    ForeColor = Color.FromArgb(125, 225, 176),
                    Dock = DockStyle.None,
                    Anchor = AnchorStyles.None,
                    Name = string.Format("TB{0}{1}", 0, i),
                    Font = new Font("Century Gothic", 10),
                    MinimumSize = new Size(150, 30)
                }, 0, i);

                tableLayoutPanel1.Controls.Add(new Button
                {
                    //Text = string.Format("Browse{0}{1}", 1, i),
                    Text = "Browse",
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(36, 46, 64),
                    ForeColor = Color.FromArgb(125, 225, 176),
                    Anchor = AnchorStyles.None,
                    Name = string.Format("BrowseBTN{0}{1}", 1, i),
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0),
                    Font = new Font("Century Gothic", 9),
                    Size = new Size(75, 30)
                }, 1, i);

                tableLayoutPanel1.Controls.Add(new PictureBox
                {
                    Image = image,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.FromArgb(36, 46, 64),
                    Dock = DockStyle.Fill,
                    Anchor = AnchorStyles.None,
                    Visible = false,
                    Name = string.Format("PB{0}{1}", 2, i)
                }, 2, i);

                tableLayoutPanel1.Controls.Add(new TextBox
                {
                    TextAlign = HorizontalAlignment.Center,
                    BackColor = Color.FromArgb(36, 46, 64),
                    ForeColor = Color.FromArgb(125, 225, 176),
                    Dock = DockStyle.Fill,
                    Anchor = AnchorStyles.None,
                    Name = string.Format("TB{0}{1}", 3, i),
                    Font = new Font("Century Gothic", 10),
                    MinimumSize = new Size(150, 30)
                }, 3, i);

                tableLayoutPanel1.Controls.Add(new Button
                {
                    //Text = string.Format("Browse{0}{1}", 4, i),
                    Text = "Browse",
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(36, 46, 64),
                    ForeColor = Color.FromArgb(125, 225, 176),
                    Anchor = AnchorStyles.None,
                    Name = string.Format("BrowseBTN{0}{1}", 4, i),
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0),
                    Font = new Font("Century Gothic", 9),
                    Size = new Size(75, 30)
                }, 4, i);

                tableLayoutPanel1.Controls.Add(new PictureBox
                {
                    Image = image,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.FromArgb(36, 46, 64),
                    Dock = DockStyle.Fill,
                    Anchor = AnchorStyles.None,
                    Visible = false,
                    Name = string.Format("PB{0}{1}", 5, i)                    
                }, 5, i);
            }
            
            AddControlsToLists();
            AssignFunctions(); //assigning fuctions to the new buttons
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            serialization.WordsToList();
            serialization.ExportFiles();
            //serialization.AddPathToReg();
            Reset();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numOfWords = (int)numericUpDown1.Value * 2;
            serialization.SetNumOfWords(numOfWords);
        }

        #endregion

        public SecondUserControl()
        {
            InitializeComponent();
            Initiate();
        }

        private void SecondUserControl_Load(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
            {
                numOfWords = (int)numericUpDown1.Value * 2;
                serialization.SetNumOfWords(numOfWords);
            }

            else
            {
                numOfWords = 10;
                serialization.SetNumOfWords(numOfWords);
            }
        }
    }
}
