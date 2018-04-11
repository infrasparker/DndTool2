using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDTool2.Model
{
    public class ObservableClass : INotifyPropertyChanged
    {
        protected ObservableClass() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string property_name)
        {
            //PropertyChangedEventHandler handler = PropertyChanged;
            //if (handler != null)
            //    handler(this, new PropertyChangedEventArgs(property_name));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
        }

        protected void PropertyUpdateList(List<string> props)
        {
            foreach (string s in props)
            {
                OnPropertyChanged(s);
            }
        }
    }
}
