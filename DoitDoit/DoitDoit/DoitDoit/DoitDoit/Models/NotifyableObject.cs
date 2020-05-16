using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;

namespace DoitDoit.Models {
    public class NotifyableObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
