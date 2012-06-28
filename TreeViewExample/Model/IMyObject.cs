using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TreeViewExample.Model
{
    public interface IMyObject
    {
        string Name { get; set; }
        ObservableCollection<IMyObject> Children { get; set; }
    }
}
