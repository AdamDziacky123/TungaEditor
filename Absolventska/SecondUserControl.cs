using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;

namespace Absolventska /////////////////////////////////////////////////////////////////////////// ak necham len jedno slovo, vyhadzuje aj chybu word alredady used
{
    public partial class SecondUserControl : UserControl//: UCManager
    {
        #region Variables
        UCManager manager = UCManager.GetInstance();

        protected List<Button> browseBTNs = new List<Button>(); //SUC browse btns
        protected List<PictureBox> PBs = new List<PictureBox>(); //SUC pictureboxes
        protected List<TextBox> TBs = new List<TextBox>(); // SUC textboxes

        protected Dictionary<int, string> paths = new Dictionary<int, string>(); //index + path
        protected Dictionary<int, string> words = new Dictionary<int, string>(); // index + words
        protected Dictionary<string, string> output = new Dictionary<string, string>(); // words + paths

        //string wordsFilePath = Environment.CurrentDirectory + "/Words.txt";
        int index = 0;
        int numOfWords;
        #endregion

        public SecondUserControl()
        {
            InitializeComponent();
            Initiate();
        }

        private void SecondUserControl_Load(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0) numOfWords = (int)numericUpDown1.Value * 2;
            else numOfWords = 10;
        }

        #region InitiateFunctions 

        private void Initiate()
        {
            ButtonsToList(); //just adding original objects do lists - in case that teacher doesnt want to change the number
            PBsToList();
            TBsToList();

            foreach (Button btn in browseBTNs) //shortening code
            {
                btn.Click += new EventHandler(OnClick);
            }

            foreach (TextBox TB in TBs) //shortening code
            {
                TB.TextChanged += new EventHandler(TbTextChanged);
            }

            btnReset.Click += new EventHandler(Reset);
        }

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

        #region OtherMethods

        private void Reset(object sender, EventArgs e)
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
        } //using two types of same function - one for button as eventHandler 

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
                OFD.Filter = "JPG files (.jpg)|*.jpg";

                if (OFD.ShowDialog() == DialogResult.OK)
                {
                    index = browseBTNs.IndexOf(btn);
                    filePath = OFD.FileName;

                    try
                    {
                        if (!paths.ContainsKey(index) && !paths.ContainsValue(filePath)) paths.Add(index, filePath);
                        else
                        {
                            MessageBox.Show("Image was already used.");
                        }

                        if (TBs[index].Text.Length > 0 && paths.ContainsKey(index)) PBs[index].Visible = true;
                    }

                    catch (System.ArgumentException e)
                    {
                        MessageBox.Show("Image already assigned. Please use the Reset button.");
                    }
                }
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            BrowseBTNFunction(btn);
        }

        private void TbTextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            index = TBs.IndexOf(tb);
            if (paths.ContainsKey(index) && tb.Text.Length > 0) PBs[index].Visible = true;
            else PBs[index].Visible = false;
        }

        private void BTNsetRows_Click(object sender, EventArgs e) // creating dynamic table + assigning objects + adding them to lists
        {
            //List<Button> BTNList = new List<Button>();
            Image image = Image.FromFile(@"D:\Podklady\Absolventská\success.png"); //Success image
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
                tableLayoutPanel1.Controls.Add(new TextBox
                {
                    TextAlign = HorizontalAlignment.Center,
                    BackColor = Color.FromArgb(36, 46, 64),
                    ForeColor = Color.FromArgb(125, 225, 176),
                    Dock = DockStyle.None,
                    Anchor = AnchorStyles.None,
                    Name = string.Format("TB{0}{1}", 0, i),
                    Font = new Font("Century Gothic", 10),
                    MinimumSize = new Size(195, 32)
                }, 0, i);

                tableLayoutPanel1.Controls.Add(new Button
                {
                    Text = string.Format("Browse{0}{1}", 1, i),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(36, 46, 64),
                    ForeColor = Color.FromArgb(125, 225, 176),
                    Anchor = AnchorStyles.None,
                    Name = string.Format("BrowseBTN{0}{1}", 1, i),
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0),
                    Font = new Font("Century Gothic", 9),
                    Size = new Size(75, 25)
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
                    MinimumSize = new Size(195, 32)
                }, 3, i);

                tableLayoutPanel1.Controls.Add(new Button
                {
                    Text = string.Format("Browse{0}{1}", 4, i),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(36, 46, 64),
                    ForeColor = Color.FromArgb(125, 225, 176),
                    Anchor = AnchorStyles.None,
                    Name = string.Format("BrowseBTN{0}{1}", 4, i),
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(0),
                    Font = new Font("Century Gothic", 9),
                    Size = new Size(75, 25)
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
            AssignFunctions();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            WordsToList();
            ExportFiles();
        }

        #endregion

        #region Exporting files
        private void WordsToList() //Into Serialization class
        {
            for (int i = 0; i < TBs.Count; i++) // checking if the word was not already used
            {
                if (!words.ContainsKey(i) && !words.ContainsValue(TBs[i].Text))
                {
                    if (TBs[i].Text.Length > 0) words.Add(i, TBs[i].Text);

                    else
                    {
                        //MessageBox.Show("Some words are missing. Please, tap the reset button");
                        break;
                    }
                }

                else
                {
                    MessageBox.Show("Word already used. Type in another word.");
                    TBs[i].Text = string.Empty;
                    PBs[i].Visible = false;
                    words.Clear();
                    break;
                }
            }

            if (Directory.Exists(manager.GetPath_files()) && Directory.GetFiles(manager.GetPath_files()).Count() > 0)
            {
                DirectoryInfo di = new DirectoryInfo(manager.GetPath_files());

                foreach (FileInfo fi in di.GetFiles())
                {
                    fi.Delete();
                }
            }

            else Directory.CreateDirectory(manager.GetPath_files());

            if (File.Exists(manager.GetPath_words())) File.Delete(manager.GetPath_words());
           
            using (StreamWriter writer = new StreamWriter(manager.GetPath_words()))
            {
                for (int i = 0; i < words.Count; i++)
                {
                    writer.WriteLine(words.ElementAt(i).Value);
                }
            }
        }

        private void ExportFiles() //Into Serialization class
        {
            if (words.Count == numOfWords && paths.Count == numOfWords)
            {
                for (int i = 0; i < TBs.Count; i++)
                {
                    output.Add(words[i], paths[i]);
                }

                //ExportToSharePoint();
                //MessageBox.Show("Output created.");

                for (int i = 0; i < output.Count; i++)
                {
                    File.Copy(output.Values.ElementAt(i), manager.GetPath_files() + "/" + output.Keys.ElementAt(i) + ".jpg", true);
                }

                Reset();
                MessageBox.Show("Files exported successfully.");
            }

            else MessageBox.Show("Missing words or pictures. Files export not successful. Tap the Reset button.");
        }
        #endregion

        #region Sharepoint
        /*private void ExportToSharePoint()
        {
            string library_name = "Documents";
            string siteUrl = "https://vzdelavameprebuducnost.sharepoint.com/sites/Private"; //"https://vzdelavameprebuducnost-my.sharepoint.com/personal/adam_dziacky_studentstc_sk/";
            string passWord = "OFK2010Kurima<3";

            SecureString securePassword = new SecureString();
            SharePointOnlineCredentials credentials;

            foreach (char c in passWord) securePassword.AppendChar(c);
            credentials = new SharePointOnlineCredentials("adam.dziacky@studentstc.sk", securePassword);

            if (output != null)
            {
                using (ClientContext ctx = new ClientContext(siteUrl))
                {
                    FileCreationInformation fcInfo = new FileCreationInformation();
                    Web myWeb = ctx.Web;
                    List myLibrary = myWeb.Lists.GetByTitle(library_name);

                    ctx.Credentials = credentials;

                    fcInfo.Url = "Words.txt";
                    fcInfo.Overwrite = true;
                    fcInfo.Content = System.IO.File.ReadAllBytes(wordsFilePath);

                    myLibrary.RootFolder.Files.Add(fcInfo);
                    myLibrary.Update();

                    ctx.ExecuteQuery();

                    foreach (var element in output)
                    {
                        fcInfo.Url = element.Key + ".jpg";
                        fcInfo.Overwrite = true;
                        //fcInfo.Content = System.IO.File.ReadAllBytes(filePath);
                        fcInfo.Content = System.IO.File.ReadAllBytes(element.Value);

                        myLibrary.RootFolder.Files.Add(fcInfo);
                        myLibrary.Update();

                        ctx.ExecuteQuery();
                    }

                }
                MessageBox.Show("Files Exported to SharePoint");
            }

            else
            {
                MessageBox.Show("Something went wrong.");
            }

            Reset();
        }

        private void NewList()
        {
            string library_name = "Documents";
            string siteUrl = "https://vzdelavameprebuducnost.sharepoint.com/sites/Private"; //"https://vzdelavameprebuducnost-my.sharepoint.com/personal/adam_dziacky_studentstc_sk/";
            string passWord = "OFK2010Kurima<3";

            SecureString securePassword = new SecureString();
            SharePointOnlineCredentials credentials;

            foreach (char c in passWord) securePassword.AppendChar(c);
            credentials = new SharePointOnlineCredentials("adam.dziacky@studentstc.sk", securePassword);

            using (ClientContext ctx = new ClientContext(siteUrl))
            {
                Web web = ctx.Web;
                ListCreationInformation info = new ListCreationInformation();
                info.Title = "New List";
                info.Description = "New Description";
                info.TemplateType = (int)ListTemplateType.Announcements;

                List myList = web.Lists.Add(info);
                myList.OnQuickLaunch = true;
                myList.Update();

                ctx.Credentials = credentials;
                ctx.ExecuteQuery();
            }
            MessageBox.Show("List created");
        }*/
        #endregion

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numOfWords = (int)numericUpDown1.Value * 2;
        }
    }
}
