using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DrawingPad.Layers
{
    public class ContainerLayer : Canvas
    {
        #region 实例变量

        private UIElementLayer elementLayer;
        private DrawableVisualLayer visualLayer;

        #endregion

        #region 构造方法

        public ContainerLayer()
        {
            this.elementLayer = new UIElementLayer();
            this.visualLayer = new DrawableVisualLayer();

            this.Children.Add(this.elementLayer);
            this.Children.Add(this.visualLayer);

            this.Background = Brushes.Transparent;
        }

        #endregion

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            Console.WriteLine("down1");
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            Console.WriteLine("up1");
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Console.WriteLine("move1");
        }
    }
}
