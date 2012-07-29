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
        private static string _currentFilePath = "";

        #region Static members

        public static void Save(ObservableCollection<Achievement> achievements)
        {
            if (_currentFilePath == "") return;

            using (var fs = new FileStream(_currentFilePath, FileMode.Open))
            {
                var serializer = new XmlSerializer(achievements.GetType());
                serializer.Serialize(fs, achievements);
            }
        }

        public static void SaveAs(ObservableCollection<Achievement> achievements)
        {
            var saveDialog = new SaveFileDialog {Filter = "Data files (*.save)|*.save;"};
            var ok = saveDialog.ShowDialog();

            if (ok.Value != true) return;

            _currentFilePath = saveDialog.FileName;
            using (var fs = saveDialog.OpenFile())
            {
                var serializer = new XmlSerializer(achievements.GetType());
                serializer.Serialize(fs, achievements);
            }
        }

        public static void Open(string filePath, ref ObservableCollection<Achievement> achievements)
        {
            var openDialog = new OpenFileDialog {Filter = "Data files (*.save)|*.save;"};
            var ok = openDialog.ShowDialog();

            if (!ok.Value) return;
            
            _currentFilePath = openDialog.FileName;
            using (var fs = openDialog.OpenFile())
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<Achievement>));
                achievements = (ObservableCollection<Achievement>)serializer.Deserialize(fs);
            }
        }

        #endregion

    }
}
