using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using TreeViewExample.ViewModel;

namespace TreeViewExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            MainWindowModel viewModel = new MainWindowModel();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();
        }
    }
}
