using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPFToolkit.MVVM;
using WPFToolkit.Utility;

namespace DrawingPad.ViewModels
{
    public class FontVM : ItemViewModel
    {
        public FontFamily FontFamily { get; private set; }

        public FontVM(FontFamily ff)
        {
            this.FontFamily = ff;
            this.Name = this.FontFamily.Source;
        }
    }

    public class FontSizeVM : ItemViewModel
    {
        public FontSizeVM(int value)
        {
            this.Name = value.ToString();
        }
    }

    public class ToolboxViewModel : ViewModelBase
    {
        public ItemsViewModel<FontVM> FontList { get; private set; }

        public ItemsViewModel<FontSizeVM> FontSizes { get; private set; }

        public ToolboxViewModel()
        {
            this.InitializeFonts();

            this.InitializeFontSize();
        }

        private void InitializeFonts()
        {
            this.FontList = new ItemsViewModel<FontVM>();

            List<FontFamily> fonts = FontUtility.GetFontFamily();

            foreach (FontFamily font in fonts)
            {
                FontVM vm = new FontVM(font);
                this.FontList.Items.Add(vm);
            }
        }

        private void InitializeFontSize()
        {
            this.FontSizes = new ItemsViewModel<FontSizeVM>();

            for (int i = 8; i < 30; i++)
            {
                FontSizeVM vm = new FontSizeVM(i);
                this.FontSizes.Items.Add(vm);
            }
        }
    }
}
