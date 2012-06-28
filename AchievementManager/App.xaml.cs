using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using AchievementManager.ViewModel;

namespace AchievementManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow();
            MainWindowModel mainModel = new MainWindowModel();
            mainWindow.DataContext = mainModel;
            mainModel.SwitchView(new AchievementViewModel());
            mainWindow.Show();
        }
    }
}
