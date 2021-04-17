using DrawingPad.Drawable;
using DrawingPad.Graphics;
using DrawingPad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WPFToolkit.DragDrop;

namespace DrawingPad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IDropHandler
    {
        #region 构造方法

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件处理器

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Console.WriteLine("父节点Down");
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("子节点Down");

            Border b = e.Source as Border;
            Console.WriteLine(b.Name);
        }

        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("父节点PreviewDown");

            Border b = e.Source as Border;
            Console.WriteLine(b.Name);
        }

        private void Border_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("子节点PreviewDown");

            Border b = e.Source as Border;
            Console.WriteLine(b.Name);
        }

        #endregion

        #region IDropHandler

        public void OnDragOver(DropInfo dropInfo)
        {
            dropInfo.Effects = DragDropEffects.Copy;
        }

        public void OnDrop(DropInfo dropInfo)
        {
            Point position = dropInfo.DropPosition;

            GraphicsVM gvm = dropInfo.Data as GraphicsVM;

            GraphicsBase graphics = GraphicsFactory.Create(position, gvm.Type);

            DrawableLayer.DrawVisual(graphics);
        }

        #endregion
    }
}
