using DrawingPad.Graphics;
using SciencePad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrawingPad.Drawable
{
    public class DrawableRectangle : DrawableVisual
    {
        #region 常量

        private const int DefaultBorderWidth = 2;

        #endregion

        #region 实例变量

        private Brush brush = PadBrushes.Background;
        private Pen borderPen = Pens.Black;

        private GraphicsRectangle graphicsRect;

        #endregion

        #region 构造方法

        public DrawableRectangle(GraphicsBase graphics) :
            base(graphics)
        {
            this.graphicsRect = graphics as GraphicsRectangle;
        }

        #endregion

        protected override void RenderCore(DrawingContext dc)
        {
            dc.DrawRectangle(this.brush, this.borderPen, this.graphicsRect.MakeRect());
        }

        public override PointCollection GetCircleTrackers()
        {
            Rect rect = this.graphicsRect.MakeRect();
            PointCollection points = new PointCollection();
            points.Add(new Point(rect.TopLeft.X, rect.TopLeft.Y + rect.Height / 2));            // 左边的点
            points.Add(new Point(rect.TopRight.X, rect.TopRight.Y + rect.Height / 2));          // 右边的点
            points.Add(new Point(rect.TopLeft.X + rect.Width / 2, rect.TopLeft.Y));             // 上边的点
            points.Add(new Point(rect.TopLeft.X + rect.Width / 2, rect.TopLeft.Y + rect.Height));       // 下边的点
            return points;
        }

        public override PointCollection GetRectangleTrackers()
        {
            Rect rect = this.graphicsRect.MakeRect();
            PointCollection points = new PointCollection();
            points.Add(rect.TopLeft);
            points.Add(rect.TopRight);
            points.Add(rect.BottomLeft);
            points.Add(rect.BottomRight);
            return points;
        }

        public override Point GetRotationPoint()
        {
            throw new NotImplementedException();
        }

        #region 实例方法

        #endregion
    }
}
