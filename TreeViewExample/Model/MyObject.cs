using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TreeViewExample.Model
{
    public class MyObject : IMyObject, INotifyPropertyChanged
    {
        private string _name = "";
        private ObservableCollection<IMyObject> _children = null;

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

        public ObservableCollection<IMyObject> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new ObservableCollection<IMyObject>();
                }
                return _children;
            }
            set
            {
                _children = value;
                OnPropertyChanged("Children");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                eventHandler(this, e);
            }
        }


    }
}
