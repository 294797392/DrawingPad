using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawingPad.Drawable
{
    public class VisualCircle : DrawableVisual
    {
        #region 常量

        private const int DefaultBorderWidth = 2;

        #endregion

        #region 实例变量

        private Pen borderPen;

        private int borderWidth = DefaultBorderWidth;

        #endregion

        #region 构造方法

        public VisualCircle(double centerX, double centerY, double radius, Brush borderBrush)
        {
            this.borderPen = new Pen(borderBrush, this.borderWidth);
        }

        public VisualCircle(Point center, double radius, Brush borderBrush)
        {
            this.borderPen = new Pen(borderBrush, this.borderWidth);
        }

        #endregion

        protected override void RenderCore(DrawingContext dc)
        {
            //Point center = new Point(centerX, centerY);
            //dc.DrawEllipse(null, borderPen, center, radius, radius);
        }

        #region 实例方法

        #endregion
    }
}
