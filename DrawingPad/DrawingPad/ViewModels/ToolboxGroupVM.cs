using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToolkit.MVVM;

namespace DrawingPad.ViewModels
{
    public class ToolboxGroupVM : ItemsViewModel<ToolboxItemVM>
    {
        public ToolboxGroupVM()
        {
        }
    }
}
