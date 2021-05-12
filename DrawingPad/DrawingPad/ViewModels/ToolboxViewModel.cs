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
        private int value;

        public int Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                this.NotifyPropertyChanged("Value");
            }
        }

        public FontSizeVM(int value)
        {
            this.Name = string.Format("{0}px", value.ToString());
        }
    }

    public class ToolboxViewModel : ViewModelBase
    {
        #region 实例变量

        private bool italic;

        private bool bold;

        private bool underline;

        #endregion

        #region 属性

        /// <summary>
        /// 斜体是否选中
        /// </summary>
        public bool Italic
        {
            get { return this.italic; }
            set 
            {
                this.italic = value;
                this.NotifyPropertyChanged("Italic");
            }
        }

        /// <summary>
        /// 粗体是否选中
        /// </summary>
        public bool Bold
        {
            get { return this.bold; }
            set
            {
                this.bold = value;
                this.NotifyPropertyChanged("Bold");
            }
        }

        /// <summary>
        /// 下划线是否选中
        /// </summary>
        public bool Underline
        {
            get { return this.underline; }
            set
            {
                this.underline = value;
                this.NotifyPropertyChanged("Underline");
            }
        }

        public ItemsViewModel<FontVM> FontList { get; private set; }

        public ItemsViewModel<FontSizeVM> FontSizes { get; private set; }

        #endregion

        #region 构造方法

        public ToolboxViewModel()
        {
            this.InitializeFonts();

            this.InitializeFontSize();
        }

        #endregion

        #region 实例方法

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

            for (int i = 12; i < 30; i++)
            {
                FontSizeVM vm = new FontSizeVM(i);
                this.FontSizes.Items.Add(vm);
            }
        }

        #endregion
    }
}
