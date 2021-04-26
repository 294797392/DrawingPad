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
        /// 获取点P相对于图形的位置
        /// 点P可以是任意一点
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static RelativeLocation GetRelativeLocation(GraphicsBase graphics, Point p)
        {
            Rect bounds = graphics.GetBounds();

            if (p.X >= bounds.TopLeft.X && p.X <= bounds.TopRight.X && p.Y <= bounds.TopLeft.Y)
            {
                // 点在图形的上方
                return RelativeLocation.Top;
            }

            if (p.X <= bounds.TopLeft.X && p.Y >= bounds.TopLeft.Y && p.Y <= bounds.BottomLeft.Y)
            {
                // 点在图形的左边
                return RelativeLocation.Left;
            }

            if (p.Y >= bounds.BottomLeft.Y && p.X >= bounds.TopLeft.X && p.X <= bounds.TopRight.X)
            {
                // 点在图形的下面
                return RelativeLocation.Bottom;
            }

            if (p.X >= bounds.TopRight.X && p.Y >= bounds.TopRight.Y && p.Y <= bounds.BottomRight.Y)
            {
                // 点在图形的右边
                return RelativeLocation.Right;
            }

            if (p.X < bounds.TopLeft.X && p.Y < bounds.TopLeft.Y)
            {
                // 点在图形的左上方
                return RelativeLocation.TopLeft;
            }

            if (p.X > bounds.TopRight.X && p.Y < bounds.TopRight.Y)
            {
                // 点在图形的右上方
                return RelativeLocation.TopRight;
            }

            if (p.X < bounds.BottomLeft.X && p.Y > bounds.BottomLeft.Y)
            {
                // 点在图形的左下方
                return RelativeLocation.BottomLeft;
            }

            if (p.X > bounds.BottomRight.X && p.Y > bounds.BottomRight.Y)
            {
                // 点在图形的右下方
                return RelativeLocation.BottomRight;
            }

            return RelativeLocation.Bottom;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstGraphics">连接线的第一个图形</param>
        /// <param name="firstConnector">第一个图形上的连接点的坐标</param>
        /// <param name="secondGraphics">连接的第二个图形</param>
        /// <param name="cursorPos">当前的鼠标坐标</param>
        /// <returns></returns>
        public static List<Point> MakeConnectionPoints(GraphicsBase firstGraphics, Point firstConnector, GraphicsBase secondGraphics, Point cursorPos)
        {
            double cursorX = cursorPos.X;
            double cursorY = cursorPos.Y;
            double startX = firstConnector.X;
            double startY = firstConnector.Y;

            if (startX == cursorX && startY == cursorY)
            {
                return null;
            }

            Point lastPoint = cursorPos;

            ConnectionLocations firstLocation = firstGraphics.GetConnectionLocation(firstConnector);

            List<Point> pointList = new List<Point>();

            pointList.Add(firstConnector);

            if (cursorX > startX && cursorY > startY)
            {
                // 往右下方拖动
                Console.WriteLine("往右下方拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 左边的点，往右下方拖动
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));

                            if (secondGraphics != null)
                            {
                                // 和另外一个图形连接起来了
                                Point connector;

                                ConnectionLocations secondLocation = secondGraphics.HitTestConnectionLocation(cursorPos, out connector);

                                lastPoint = connector;

                                switch (secondLocation)
                                {
                                    case ConnectionLocations.Bottom:
                                        {
                                            pointList.Add(new Point(startX - PadContext.MinimalMargin, cursorY - PadContext.MinimalMargin));
                                            pointList.Add(new Point(cursorX, cursorY - PadContext.MinimalMargin));
                                            break;
                                        }

                                    case ConnectionLocations.Left:
                                        {

                                            break;
                                        }

                                    case ConnectionLocations.Right:
                                        {
                                            break;
                                        }

                                    case ConnectionLocations.Top:
                                        {
                                            pointList.Add(new Point(startX - PadContext.MinimalMargin, cursorY - PadContext.MinimalMargin));
                                            pointList.Add(new Point(cursorX, cursorY - PadContext.MinimalMargin));
                                            break;
                                        }

                                    case ConnectionLocations.Null:
                                        {
                                            // 在第二个图形的中间或者边上
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                pointList.Add(new Point(startX - PadContext.MinimalMargin, cursorY));
                            }

                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 上边的点，往右下方拖动
                            pointList.Add(new Point(startX, startY - PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY - PadContext.MinimalMargin));
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 右边的点，往右下方拖动
                            pointList.Add(new Point(cursorX, startY));
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 下边的点，往右下方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }
                }
            }
            else if (cursorX > startX && cursorY < startY)
            {
                // 往右上方拖动
                Console.WriteLine("往右上方拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 左边的点，往右上方拖动
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 上边的点，往右上方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 右边的点，往右上方拖动
                            pointList.Add(new Point(cursorX, startY));
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 下边的点，往右上方拖动
                            pointList.Add(new Point(startX, startY + PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY + PadContext.MinimalMargin));
                            break;
                        }
                }
            }
            else if (cursorX < startX && cursorY > startY)
            {
                // 往左下方拖动
                Console.WriteLine("往左下方拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 左边的点，往左下方拖动
                            pointList.Add(new Point(cursorX, firstConnector.Y));
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 上边的点，往左下方拖动
                            pointList.Add(new Point(startX, startY - PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY - PadContext.MinimalMargin));
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 右边的点，往左下方拖动
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 下边的点，往左下方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }
                }
            }
            else if (cursorX < startX && cursorY < startY)
            {
                // 往左上方拖动
                Console.WriteLine("往左上方拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 左边的点，往左上方拖动
                            pointList.Add(new Point(cursorX, firstConnector.Y));
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 上边的点，往左上方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 右边的点，往左上方拖动
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 下边的点，往左上方拖动
                            pointList.Add(new Point(startX, startY + PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY + PadContext.MinimalMargin));
                            break;
                        }
                }
            }
            else if (cursorX > startX && cursorY == startY)
            {
                // 往右拖动
                Console.WriteLine("往右拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            break;
                        }
                }
            }
            else if (cursorX < startX && cursorY == startY)
            {
                // 往左拖动
                Console.WriteLine("往左拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            break;
                        }
                }
            }
            else if (cursorX == startX && cursorY > startY)
            {
                // 往下拖动
                Console.WriteLine("往下拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            break;
                        }
                }
            }
            else if (cursorX == startX && cursorY < startY)
            {
                // 往上拖动
                Console.WriteLine("往上拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            break;
                        }
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            pointList.Add(lastPoint);

            return pointList;
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

        /// <summary>
        /// 生成一个矩形
        /// </summary>
        /// <param name="center">矩形的中心坐标</param>
        /// <param name="radius">矩形的半径</param>
        /// <returns></returns>
        public static Rect MakeRect(Point center, int radius)
        {
            return MakeRect(new Point(center.X - radius, center.Y - radius), new Point(center.X + radius, center.Y + radius));
        }
    }
}
