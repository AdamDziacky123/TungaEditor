using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Absolventska
{
    class SerializationClass 
    {
        UCManager manager = UCManager.GetInstance();

        public List<Button> browseBTNs = new List<Button>(); //SUC browse btns
        public List<PictureBox> PBs = new List<PictureBox>(); //SUC pictureboxes
        public List<TextBox> TBs = new List<TextBox>(); // SUC textboxes
        
        public Dictionary<int, string> paths = new Dictionary<int, string>(); //index + path
        public Dictionary<int, string> words = new Dictionary<int, string>(); // index + words
        public Dictionary<string, string> output = new Dictionary<string, string>(); // words + paths

        private static SerializationClass instance = new SerializationClass();
        private int numOfWords;

        public void SetNumOfWords(int num) //used in dynamic table
        {
            numOfWords = num;
        }

        public static SerializationClass GetInstance()
        {
            return instance;
        }

        public void AddPathToReg()
        {
            if (Environment.GetEnvironmentVariable("Tunga", EnvironmentVariableTarget.User) == null)
            {
                Environment.SetEnvironmentVariable("Tunga", manager.GetPath_files(), EnvironmentVariableTarget.User);
                MessageBox.Show("Register variable created.");
            }

            else
            {
                if (Environment.GetEnvironmentVariable("Tunga", EnvironmentVariableTarget.User) != manager.GetPath_files())
                {
                    Environment.SetEnvironmentVariable("Tunga", manager.GetPath_files(), EnvironmentVariableTarget.User);
                    MessageBox.Show("Register Variable Updated");
                }

               /* else
                {
                    MessageBox.Show("Register already exists.");
                }*/
            }
        } //adding current path_files to register for the app

        public void WordsToList()
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
                writer.WriteLine(numOfWords);

                for (int i = 0; i < words.Count; i++)
                {
                    writer.WriteLine(words.ElementAt(i).Value);
                }
            }
        }

        public void ExportFiles()
        {
            if (words.Count == numOfWords && paths.Count == numOfWords)
            {
                for (int i = 0; i < TBs.Count; i++)
                {
                    output.Add(words[i], paths[i]);
                }

                for (int i = 0; i < output.Count; i++)
                {
                    File.Copy(output.Values.ElementAt(i), manager.GetPath_files() + "/" + output.Keys.ElementAt(i) + ".jpg", true);
                }

                MessageBox.Show("Files exported successfully.");
            }

            else MessageBox.Show("Missing words or pictures. Files export not successful. Tap the Reset button.");
        }
    }
}
