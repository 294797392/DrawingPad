using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawingPad.Drawable
{
    public class DrawableLine : DrawableVisual
    {
        #region 实例变量

        private GraphicsLine graphics;

        #endregion

        #region 属性

        public override int CircleHandles { get; protected set; }
        public override int RectangleHandles { get; protected set; }

        #endregion

        #region 构造方法

        public DrawableLine(GraphicsBase graphics) : base(graphics)
        {
            this.graphics = graphics as GraphicsLine;
        }

        #endregion

        #region DrawableVisual

        public override Point GetCircleHandle(int num)
        {
            return new Point();
        }

        public override Point GetRectangleHandle(int num)
        {
            return new Point();
        }

        public override Point GetRotationHandle()
        {
            return new Point();
        }

        protected override void RenderCore(DrawingContext dc)
        {
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawables"></param>
        /// <param name="startVisual">连接线起始元素</param>
        /// <param name="visualHit">当前被鼠标命中的元素，说明鼠标在某个图形里</param>
        /// <param name="cursorPosition"></param>
        public void Update(IEnumerable<DrawableVisual> drawables, DrawableVisual startVisual, DrawableVisual visualHit, Point cursorPosition)
        {
            DrawingContext dc = this.RenderOpen();

            Point startPoint = this.graphics.StartPoint;
            double cursorX = cursorPosition.X;
            double cursorY = cursorPosition.Y;
            double startX = startPoint.X;
            double startY = startPoint.Y;

            if (startX == cursorX && startY == cursorY)
            {
                return;
            }

            Rect bounds = startVisual.ContentBounds;                // 图形的边界框

            dc.Close();
        }
    }
}
