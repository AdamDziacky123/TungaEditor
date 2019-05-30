using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Absolventska
{
    public class UCManager
    {
        List<UserControl> controls = new List<UserControl>();
        List<Panel> panels = new List<Panel>();
        List<Button> buttons = new List<Button>();

        Point UCPosition;
        protected string path_words = Environment.CurrentDirectory + @"\Tunga Files\Words.txt"; //save environment dir to string
        protected string path_files = Environment.CurrentDirectory + @"\Tunga Files";
        private static UCManager instance = new UCManager();
            
        public static UCManager GetInstance()
        {
            return instance;
        }

        #region PathMethods

        public void SetPath_files(string newPathFiles, bool setDefault)
        {
            if(!setDefault) path_files = newPathFiles;
            else path_files = Environment.CurrentDirectory + @"\Tunga Files";
        }

        public void SetPath_words(string newPathWords)
        {
            path_words = newPathWords;
        }

        public string GetPath_files()
        {
            return path_files;
        }

        public string GetPath_words()
        {
            return path_words;
        }

        #endregion

        #region ListMethods
        public void Initiate(Form1 form)
        {
            AddButtonsToList(form);
            AddPanelsToList(form);
            AddTasksToList(form);
        }

        protected void AddTasksToList(Form1 form)
        {
            controls.Add(form.Controls.Find("firstUserControl1",true).FirstOrDefault() as UserControl);
            controls.Add(form.Controls.Find("secondUserControl1", true).FirstOrDefault() as UserControl);
            controls.Add(form.Controls.Find("settingsUserControl1", true).FirstOrDefault() as UserControl);
        }

        private void AddPanelsToList(Form1 form)
        {
            panels.Add(form.Controls.Find("sidePanel", true).FirstOrDefault() as Panel);
            panels.Add(form.Controls.Find("panel3", true).FirstOrDefault() as Panel);
        }

        private void AddButtonsToList(Form1 form)
        {
            buttons.Add(form.Controls.Find("btnWelcome", true).FirstOrDefault() as Button);
            buttons.Add(form.Controls.Find("btnPictures", true).FirstOrDefault() as Button);
            buttons.Add(form.Controls.Find("btnSettings", true).FirstOrDefault() as Button);
        }
        #endregion

        public void ControlToFront(int index)
        {
            foreach (Control c in controls)
            {
                c.Visible = false;
            }

            //MessageBox.Show(Screen.PrimaryScreen.Bounds.Width.ToString());
            if (Screen.PrimaryScreen.Bounds.Width == 1920 && Screen.PrimaryScreen.Bounds.Height == 1080) UCPosition = new Point(472, 150);
            else if (Screen.PrimaryScreen.Bounds.Width == 1366 && Screen.PrimaryScreen.Bounds.Height == 768) UCPosition = new Point(300, 80);

            controls[index].Location = UCPosition;
            controls[index].Visible = true;
            controls[index].BringToFront();
        }

        public void SetPanels(int indexControl)
        {
            panels[0].Height = buttons[indexControl].Height;
            panels[0].Top = buttons[indexControl].Top;
            panels[0].BringToFront();
            panels[1].BringToFront();
        }

    }
}
