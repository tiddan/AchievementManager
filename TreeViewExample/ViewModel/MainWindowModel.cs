using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreeViewExample.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;


namespace TreeViewExample.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        private ObservableCollection<MyObject> _items = null;
        private RelayCommand<RoutedEventArgs> _selectedItemChangedCommand = null;
        private object _paramA = null;

        public ObservableCollection<MyObject> Children
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<MyObject>();
                }
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public ICommand SelectedItemChangedCommand
        {
            get
            {
                if (_selectedItemChangedCommand == null)
                {
                    _selectedItemChangedCommand = new RelayCommand<RoutedEventArgs>(SelectedItemChangedCommandExecute);
                }
                return _selectedItemChangedCommand;
            }
        }

        public object ParamA
        {
            get
            {
                return _paramA;
            }
            set
            {
                _paramA = value;
                OnPropertyChanged("ParamA");
            }
        }

        public void SelectedItemChangedCommandExecute(RoutedEventArgs e)
        {
            Console.WriteLine("Hello world");
        }

        public MainWindowModel()
        {
            Children.Add(new MyObject { Name = "Anders" });
            Children.Add(new MyObject { Name = "Heidi" });
            Children.Add(new MyObject { Name = "Emilie" });
            Children[2].Children.Add(new MyObject { Name = "Bamsefar" });

        }
    }
}
