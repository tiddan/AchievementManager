using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AchievementManager.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Windows.Forms;

namespace AchievementManager.Data
{
    public class DataStore
    {
        // Stores the last accessed file.
        private static string CurrentFilePath = "";

        //////////////////////////////////
        //
        // [STATIC MEMBERS]
        //
        //////////////////////////////////

        public static void Save(ObservableCollection<Achievement> achievements)
        {
            if (CurrentFilePath != "")
            {
                using (FileStream fs = new FileStream(CurrentFilePath, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(achievements.GetType());
                    serializer.Serialize(fs, achievements);
                }
            }
        }

        public static void SaveAs(ObservableCollection<Achievement> achievements)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Data files (*.save)|*.save;";
            bool? ok = saveDialog.ShowDialog();

            if (ok.HasValue && ok.Value == true)
            {
                CurrentFilePath = saveDialog.FileName;

                using (Stream fs = saveDialog.OpenFile())
                {
                    XmlSerializer serializer = new XmlSerializer(achievements.GetType());
                    serializer.Serialize(fs, achievements);
                }
            }
        }

        public static void Open(string filePath, ref ObservableCollection<Achievement> achievements)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Data files (*.save)|*.save;";
            bool? ok = openDialog.ShowDialog();

            if (ok.HasValue && ok.Value==true)
            {
                CurrentFilePath = openDialog.FileName;

                using (Stream fs = openDialog.OpenFile())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Achievement>));
                    achievements = (ObservableCollection<Achievement>)serializer.Deserialize(fs);
                }
            }
        }
    }
}
