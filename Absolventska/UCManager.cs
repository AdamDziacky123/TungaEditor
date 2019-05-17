using System;
using System.Collections.Generic;
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

        protected string path_words = Environment.CurrentDirectory + @"\Tunga Files/Words.txt";
        protected string path_files = Environment.CurrentDirectory + @"\Tunga Files";

        private static UCManager instance = new UCManager();

        //SecondUserControl content
        //

        public static UCManager GetInstance()
        {
            return instance;
        }

        #region PathMethods

        public void SetPath_files(string newPathFiles)
        {
            path_files = newPathFiles;
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

        public void ControlToFront(int index)
        {
            foreach (Control c in controls)
            {
                c.Visible = false;
                //MessageBox.Show(c.Name);
            }

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
