using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DrawingPad.Layers
{
    public class ContainerLayer : DrawingLayer
    {
        #region 公开属性

        public DrawableVisualLayer VisualLayer { get; private set; }

        public UIElementLayer ElementLayer { get; private set; }

        #endregion

        #region 构造方法

        public ContainerLayer()
        {
            this.VisualLayer = new DrawableVisualLayer();
            this.Children.Add(this.VisualLayer);

            this.ElementLayer = new UIElementLayer();
            this.Children.Add(this.ElementLayer);
        }

        #endregion
    }
}
