using DrawingPad.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawingPad.Graphics
{
    public static class GraphicsUtility
    {
        public static Point GetCenter(this Rect rect)
        {
            return new Point(rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height / 2);
        }

        /// <summary>
        /// 获取点p相对于visual的顶点位置
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static GraphicsVertexPosition GetVertex(DrawableVisual visual, Point p)
        {
            Rect bounds = visual.GetBounds();

            Point leftTop = bounds.Location;

            if (p.X == leftTop.X && p.Y == leftTop.Y + bounds.Height / 2)
            {
                return GraphicsVertexPosition.CenterLeft;
            }
            else if (p.X == leftTop.X + bounds.Width / 2 && p.Y == leftTop.Y)
            {
                return GraphicsVertexPosition.CenterTop;
            }
            else if (p.X == leftTop.X + bounds.Width / 2 && p.Y == leftTop.Y + bounds.Height)
            {
                return GraphicsVertexPosition.CenterBottom;
            }
            else if (p.X == leftTop.X + bounds.Width && p.Y == leftTop.Y + bounds.Height / 2)
            {
                return GraphicsVertexPosition.CenterRight;
            }
            else if (p.X == leftTop.X && p.Y == leftTop.Y)
            {
                return GraphicsVertexPosition.LeftTop;
            }
            else if (p.X == bounds.TopRight.X && p.Y == bounds.TopRight.Y)
            {
                return GraphicsVertexPosition.RightTop;
            }
            else if (p.X == bounds.BottomLeft.X && p.Y == bounds.BottomLeft.Y)
            {
                return GraphicsVertexPosition.LeftBottom;
            }
            else if (p.X == bounds.BottomRight.X && p.Y == bounds.BottomRight.Y)
            {
                return GraphicsVertexPosition.RightBottom;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// 左上角和右下角组成一个矩形
        /// </summary>
        /// <param name="leftTop"></param>
        /// <param name="rightBottom"></param>
        /// <returns></returns>
        public static Rect MakeRect(Point leftTop, Point rightBottom)
        {
            Rect rect = new Rect()
            {
                Location = leftTop,
                Width = rightBottom.X - leftTop.X,
                Height = rightBottom.Y - leftTop.Y
            };
            return rect;
        }
    }
}
