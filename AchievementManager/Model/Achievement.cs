using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using GalaSoft.MvvmLight.Command;
using AchievementManager.ViewModel;

namespace AchievementManager.Model
{
    public class Achievement : ModelBase
    {
        //////////////////////////////////
        //
        // [VARIABLES]
        //
        //////////////////////////////////

        private string _name;
        private double _percentDouble = 0;
        private string _percent = "0*";
        private string _percentRest = "100*";

        private RelayCommand _addCommentCommand;
        private RelayCommand _removeCommentCommand;
        private Comment _selectedComment = null;

        private ObservableCollection<Achievement> _subAchievements = null;

        //////////////////////////////////
        //
        // [PROPERTIES]
        //
        //////////////////////////////////

        private ObservableCollection<Comment> _comments = null;

        public ObservableCollection<Achievement> SubAchievements
        {
            get
            {
                if (_subAchievements == null)
                {
                    _subAchievements = new ObservableCollection<Achievement>();
                }
                return _subAchievements;
            }
            set
            {
                _subAchievements = value;
                OnPropertyChanged("SubAchievements");
            }
        }

        public double PercentDouble
        {
            get
            {
                return _percentDouble;
            }
            set
            {
                _percentDouble = value;
                OnPropertyChanged("PercentDouble");

                if (SubAchievements.Count == 0)
                {
                    SelectionViewModel.FireRefreshPercent();
                }

                Percent = (value * 100) + "*";
                PercentRest = (100-(value * 100)) + "*";
            }
        }

        public string Percent
        {
            get
            {
                return _percent;
            }
            set
            {
                _percent = value;
                OnPropertyChanged("Percent");
            }
        }

        public string PercentRest
        {
            get
            {
                return _percentRest;
            }
            set
            {
                _percentRest = value;
                OnPropertyChanged("PercentRest");
            }
        }

        public bool IsReadOnly
        {
            get
            {
                if (SubAchievements.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<Comment> Comments
        {
            get
            {
                if (_comments == null)
                {
                    _comments = new ObservableCollection<Comment>();
                }
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged("Comments");
            }
        }

        public Comment SelectedComment
        {
            get
            {
                return _selectedComment;
            }
            set
            {
                _selectedComment = value;
                OnPropertyChanged("SelectedComment");
            }
        }
        
        //////////////////////////////////
        //
        // [COMMANDS]
        //
        //////////////////////////////////

        public ICommand AddCommentCommand
        {
            get
            {
                if (_addCommentCommand == null)
                {
                    _addCommentCommand = new RelayCommand(AddCommentCommandExecute);
                }
                return _addCommentCommand;
            }
        }

        public ICommand RemoveCommentCommand
        {
            get
            {
                if (_removeCommentCommand == null)
                {
                    _removeCommentCommand = new RelayCommand(RemoveCommentCommandExecute);
                }
                return _removeCommentCommand;
            }
        }

        //////////////////////////////////
        //
        // [METHODS]
        //
        //////////////////////////////////

        private void AddCommentCommandExecute()
        {
            Comments.Add(new Comment { Text = "New comment", TimeStamp = DateTime.Now });
        }

        private void RemoveCommentCommandExecute()
        {
            if (SelectedComment != null)
            {
                Comments.Remove(SelectedComment);
            }
        }

        public void RefreshPercentageDouble()
        {
            if (SubAchievements.Count > 0)
            {
                double sum = 0;
                foreach (Achievement a in SubAchievements)
                {
                    sum += a.PercentDouble;
                }
                sum /= SubAchievements.Count;
                PercentDouble = Math.Round(sum,4);
            }
        }
    }

}
