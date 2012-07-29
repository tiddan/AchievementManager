using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AchievementManager.Model;
using System.Drawing;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;

namespace AchievementManager.ViewModel
{
    public class SelectionViewModel : ViewModelBase
    {
        #region Variables

        private RelayCommand _addAchievementCommand;
        private RelayCommand _removeAchievementCommand;
        private RelayCommand<RoutedEventArgs> _selectedItemChangedCommand;
        private ObservableCollection<Achievement> _achievements = null;
        private Achievement _selectedAchievement = null;
        private double _listBoxWidth = 0;

        #endregion

        #region Events

        public static event EventHandler RefreshPercentage;

        #endregion

        #region Properties

        public ObservableCollection<Achievement> Achievements
        {
            get
            {
                if (_achievements == null)
                {
                    _achievements = new ObservableCollection<Achievement>();
                }
                return _achievements;
            }
            set
            {
                _achievements = value;
                OnPropertyChanged("Achievements");
            }
        }

        public Achievement SelectedAchievement
        {
            get
            {
                return _selectedAchievement;
            }
            set
            {
                _selectedAchievement = value;
                OnPropertyChanged("SelectedAchievement");
            }
        }

        public double ListBoxWidth
        {
            get
            {
                return _listBoxWidth;
            }
            set
            {
                _listBoxWidth = value;
                OnPropertyChanged("ListBoxWidth");
            }
        }

        #endregion

        #region Commands

        public ICommand AddAchievementCommand
        {
            get
            {
                if (_addAchievementCommand == null)
                {
                    _addAchievementCommand = new RelayCommand(AddAchievementCommandExecute);
                }
                return _addAchievementCommand;
            }
        }

        public ICommand RemoveAchievementCommand
        {
            get
            {
                if (_removeAchievementCommand == null)
                {
                    _removeAchievementCommand = new RelayCommand(RemoveAchievementCommandExecute);
                }
                return _removeAchievementCommand;
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

        #endregion

        #region Constructors

        public SelectionViewModel()
        {
            Achievements.Add(new Achievement { Name = "Life achievements" });
            RefreshPercentage += new EventHandler(SelectionViewModel_RefreshPercentage);
        }

        #endregion

        #region Members

        private void SelectionViewModel_RefreshPercentage(object sender, EventArgs e)
        {
            if (Achievements.Count > 0)
            {
                RefreshSubAchievements(Achievements[0]);
            }
        }

        private void RefreshSubAchievements(Achievement achievement)
        {
            achievement.RefreshPercentageDouble();

            foreach (Achievement a in achievement.SubAchievements)
            {
                RefreshSubAchievements(a);
            }
        }

        public static void FireRefreshPercent()
        {
            RefreshPercentage(null, new EventArgs());
        }

        public void AddAchievementCommandExecute()
        {
            if (SelectedAchievement != null)
            {
                SelectedAchievement.SubAchievements.Add(new Achievement { Name = "Untitled" });
            }
        }

        public void RemoveAchievementCommandExecute()
        {
            if (SelectedAchievement != null)
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Really delete : " + SelectedAchievement.Name + " ?", "Confirm delete", MessageBoxButton.YesNo))
                {
                    SearchAndRemove(Achievements[0].SubAchievements, SelectedAchievement);
                }
            }
        }

        private void SearchAndRemove(ObservableCollection<Achievement> list, Achievement toDelete)
        {
            foreach (Achievement a in list)
            {
                if (a == toDelete)
                {
                    list.Remove(toDelete);
                    return;
                }
                else
                {
                    if (a.SubAchievements.Count > 0)
                    {
                        SearchAndRemove(a.SubAchievements, toDelete);
                    }
                }
            }
        }

        public void SelectedItemChangedCommandExecute(RoutedEventArgs args)
        {
            SelectedAchievement = ((args.Source as TreeView).SelectedItem as Achievement);
        }

        #endregion

    }
}
