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
        #region Variables

        private SelectionViewModel _selectionViewModel = null;

        #endregion

        #region Properties

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

        #endregion

        #region Constructors

        public AchievementViewModel()
        {
            SelectionViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(SelectionViewModel_PropertyChanged);
        }

        #endregion

        #region Members

        private void SelectionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedAchievement")
            {
                OnPropertyChanged("SelectionViewModel");
                OnPropertyChanged("SelectionViewModel.SelectedAchievement");
            }
        }

        #endregion
    }
}
