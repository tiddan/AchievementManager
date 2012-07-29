using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using AchievementManager.Data;
using AchievementManager.Model;
using GalaSoft.MvvmLight.Command;

namespace AchievementManager.ViewModel
{
    public class MainWindowModel : ViewModelBase
    {
        #region Variables

        private RelayCommand _saveCommand;
        private RelayCommand _saveAsCommand;
        private RelayCommand _openCommand;
        private RelayCommand _newCommand;
        private RelayCommand _increment;
        private RelayCommand _decrement;
        private RelayCommand _up;
        private RelayCommand _down;

        #endregion

        #region Constructors

        public MainWindowModel()
        {
        }

        #endregion

        #region Properties

        private ObservableCollection<ViewModelBase> _viewModels = null;

        public ObservableCollection<ViewModelBase> ViewModels
        {
            get
            {
                if (_viewModels == null)
                {
                    _viewModels = new ObservableCollection<ViewModelBase>();

                }
                return _viewModels;
            }
            set
            {
                _viewModels = value;
                OnPropertyChanged("ViewModels");
            }
        }

        #endregion

        #region Commands

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(SaveCommandExecute);
                }
                return _saveCommand;
            }
        }

        public ICommand SaveAsCommand
        {
            get
            {
                if (_saveAsCommand == null)
                {
                    _saveAsCommand = new RelayCommand(SaveAsCommandExecute);
                }
                return _saveAsCommand;
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand(OpenCommandExecute);
                }
                return _openCommand;
            }
        }

        public ICommand NewCommand
        {
            get
            {
                if (_newCommand == null)
                {
                    _newCommand = new RelayCommand(NewCommandExecute);
                }
                return _newCommand;
            }
        }

        public ICommand Up
        {
            get
            {
                if (_up == null)
                {
                    _up = new RelayCommand(UpExecute);
                }
                return _up;
            }
        }

        public ICommand Down
        {
            get
            {
                if (_down == null)
                {
                    _down = new RelayCommand(DownExecute);
                }
                return _down;
            }
        }

        public ICommand Increment
        {
            get
            {
                if (_increment == null)
                {
                    _increment = new RelayCommand(IncrementExecute);
                }
                return _increment;
            }
        }

        public ICommand Decrement
        {
            get
            {
                if (_decrement == null)
                {
                    _decrement = new RelayCommand(DecrementExecute);
                }
                return _decrement;
            }
        }

        #endregion

        #region Members

        public void SwitchView(ViewModelBase newView)
        {
            ViewModels.Clear();
            ViewModels.Add(newView);
        }

        public void SaveCommandExecute()
        {
            DataStore.Save((ViewModels[0] as AchievementViewModel).SelectionViewModel.Achievements);
        }

        public void SaveAsCommandExecute()
        {
            DataStore.SaveAs((ViewModels[0] as AchievementViewModel).SelectionViewModel.Achievements);
        }

        public void OpenCommandExecute()
        {
            string filePath = @"C:\Svn\MySave.save";
            ObservableCollection<Achievement> list = (ViewModels[0] as AchievementViewModel).SelectionViewModel.Achievements;
            DataStore.Open(filePath, ref list);
            (ViewModels[0] as AchievementViewModel).SelectionViewModel.Achievements = list;

        }

        public void NewCommandExecute()
        {
            (ViewModels[0] as AchievementViewModel).SelectionViewModel.Achievements = new ObservableCollection<Achievement>();
            (ViewModels[0] as AchievementViewModel).SelectionViewModel.SelectedAchievement = null;
        }

        public void UpExecute()
        {
        }

        public void DownExecute()
        {
        }
        
        public void IncrementExecute()
        {
        }

        public void DecrementExecute()
        {
        }

        #endregion

    }
}
