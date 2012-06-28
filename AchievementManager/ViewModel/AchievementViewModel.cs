using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using AchievementManager.Model;

namespace AchievementManager.ViewModel
{
    public class AchievementViewModel : ViewModelBase
    {
        //////////////////////////////////
        //
        // [VARIABLES]
        //
        //////////////////////////////////

        private SelectionViewModel _selectionViewModel = null;

        //////////////////////////////////
        //
        // [PROPERTIES]
        //
        //////////////////////////////////

        public SelectionViewModel SelectionViewModel
        {
            get
            {
                if (_selectionViewModel == null)
                {
                    _selectionViewModel = new SelectionViewModel();
                }
                return _selectionViewModel;
            }
            set
            {
                _selectionViewModel = value;
                OnPropertyChanged("SelectionViewModel");
            }
        }

        public Visibility InfoComponentVisible
        {
            get
            {
                if (SelectionViewModel.SelectedAchievement != null)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }

        //////////////////////////////////
        //
        // [CONSTRUCTOR]
        //
        //////////////////////////////////

        public AchievementViewModel()
        {
            SelectionViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SelectionViewModel_PropertyChanged);
        }

        //////////////////////////////////
        //
        // [MEMBERS]
        //
        //////////////////////////////////

        private void SelectionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedAchievement")
            {
                OnPropertyChanged("SelectionViewModel");
                OnPropertyChanged("SelectionViewModel.SelectedAchievement");
            }
        }
    }
}
