using SciencePad.Scenes.Sine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SciencePad.UserControls
{
    /// <summary>
    /// SineSceneUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class SineSceneUserControl : UserControl
    {
        #region 常量

        private const int DefaultLineThickness = 2;

        #endregion

        #region 实例变量

        #endregion

        #region 构造方法

        public SineSceneUserControl()
        {
            InitializeComponent();

            this.InitializeUserControl();
        }

        #endregion

        #region 实例方法

        private void InitializeUserControl()
        {
            DataGridSineList.ItemsSource = SineScene.SineList;
        }

        #endregion

        #region 事件处理器

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            SineFunction sineFunc = new SineFunction() 
            {
                Amplitude = 1,
                Frequency = 1,
                Phase = 0,
                LinePen = PadUtility.RandomColorPen(DefaultLineThickness)
            };
            SineScene.SineList.Add(sineFunc);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            SineFunction selectedFunc = DataGridSineList.SelectedItem as SineFunction;
            if (selectedFunc == null)
            {
                return;
            }

            if (!PadMessage.Confirm("确定要删除选中的正弦波吗?"))
            {
                return;
            }

            SineScene.SineList.Remove(selectedFunc);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SineScene.Redraw();
        }

        #endregion
    }
}
