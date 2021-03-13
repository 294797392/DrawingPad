using DrawingPad.Graphics;
using SciencePad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        #region 属性

        public override int CircleHandles { get; protected set; }

        public override int RectangleHandles { get; protected set; }

        #endregion

        #region 构造方法

        public DrawableRectangle(GraphicsBase graphics) :
            base(graphics)
        {
            this.graphicsRect = graphics as GraphicsRectangle;
            this.CircleHandles = 4;
            this.RectangleHandles = 4;
        }

        #endregion

        protected override void RenderCore(DrawingContext dc)
        {
            dc.DrawRectangle(this.brush, this.borderPen, this.graphicsRect.MakeRect());
        }

        public override Point GetRotationHandle()
        {
            throw new NotImplementedException();
        }

        public override Point GetCircleHandle(int num)
        {
            if (num == 0)
            {
                // 左
                return new Point(this.graphicsRect.Point1X, this.graphicsRect.Point1Y + this.graphicsRect.Height / 2);
            }
            else if (num == 1)
            {
                // 上
                return new Point(this.graphicsRect.Point1X + this.graphicsRect.Width / 2, this.graphicsRect.Point1Y);
            }
            else if (num == 2)
            {
                // 右
                return new Point(this.graphicsRect.Point1X + this.graphicsRect.Width, this.graphicsRect.Point1Y + this.graphicsRect.Height / 2);
            }
            else if (num == 3)
            {
                // 下
                return new Point(this.graphicsRect.Point1X + this.graphicsRect.Width / 2, this.graphicsRect.Point1Y + this.graphicsRect.Height);
            }

            return new Point();
        }

        public override Point GetRectangleHandle(int num)
        {
            if (num == 0)
            {
                // 左上角
                return new Point(this.graphicsRect.Point1X, this.graphicsRect.Point1Y);
            }
            else if (num == 1)
            {
                // 右上角
                return new Point(this.graphicsRect.Point1X + this.graphicsRect.Width, this.graphicsRect.Point1Y);
            }
            else if (num == 2)
            {
                // 左下角
                return new Point(this.graphicsRect.Point1X, this.graphicsRect.Point1Y + this.graphicsRect.Height);
            }
            else if (num == 3)
            {
                // 右下角
                return new Point(this.graphicsRect.Point1X + this.graphicsRect.Width, this.graphicsRect.Point1Y + this.graphicsRect.Height);
            }

            return new Point();
        }

        public Cursor GetRectangleHandleCursor(int num)
        {
            throw new NotImplementedException();
            //Point center = this.GetRectangleHandle(num);
        }

        #region 实例方法

        #endregion
    }
}
