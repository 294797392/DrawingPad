using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.ViewModels
{
    public class ToolboxGroupVM : ViewModelBase
    {
        public ObservableCollection<ToolboxItemVM> ToolboxItems { get; private set; }

        public ToolboxGroupVM()
        {
            this.ToolboxItems = new ObservableCollection<ToolboxItemVM>();
        }
    }
}
