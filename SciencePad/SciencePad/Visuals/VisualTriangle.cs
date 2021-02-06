using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SciencePad.Visuals
{
    public class VisualTriangle : VisualGeometry
    {
        #region 常量

        private const int DefaultBorderWidth = 2;

        #endregion

        #region 实例变量

        private Pen pen;

        private Point p1;
        private Point p2;
        private Point p3;

        #endregion

        #region 构造方法

        public VisualTriangle(Point p1, Point p2, Point p3, Brush borderBrush)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;

            this.pen = new Pen(borderBrush, DefaultBorderWidth);
        }

        #endregion

        protected override void RenderCore(DrawingContext dc)
        {
            dc.DrawLine(this.pen, p1, p2);
            dc.DrawLine(this.pen, p2, p3);
            dc.DrawLine(this.pen, p3, p1);
        }

        #region 实例方法

        #endregion
    }
}
