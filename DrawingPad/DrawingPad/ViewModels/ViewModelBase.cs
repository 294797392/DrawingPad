using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private string id;
        private string name;
        private string description;

        public string ID
        {
            get { return this.id; }
            set
            {
                this.id = value;
                this.NotifyPropertyChanged("ID");
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return this.description; }
            set
            {
                this.description = value;
                this.NotifyPropertyChanged("Description");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
