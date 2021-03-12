using DrawingPad.Drawable;
using DrawingPad.Graphics;
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

namespace SciencePad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GraphicsRectangle graphicsRect = new GraphicsRectangle() 
            {
                Point1X = 100,
                Point1Y = 100,
                Width = 100,
                Height = 100
            };

            DrawableLayer.DrawVisual(graphicsRect);

            GraphicsRectangle graphicsRect1 = new GraphicsRectangle()
            {
                Point1X = 300,
                Point1Y = 300,
                Width = 100,
                Height = 100
            };

            DrawableLayer.DrawVisual(graphicsRect1);
        }
    }
}
