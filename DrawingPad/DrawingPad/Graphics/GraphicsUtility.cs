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
        /// <summary>
        /// 获取矩形的中心点
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
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
        /// 计算两个点之间的折线的点
        /// </summary>
        /// <param name="firstConnector">第一个连接点</param>
        /// <param name="firstLocation">第一个连接点的位置</param>
        /// <param name="secondConnector">第二个连接点</param>
        /// <returns></returns>
        public static List<Point> MakeConnectionPoints(Point firstConnector, ConnectionLocations firstLocation, Point secondConnector)
        {
            List<Point> pointList = new List<Point>();

            double secondX = secondConnector.X;
            double secondY = secondConnector.Y;
            double firstX = firstConnector.X;
            double firstY = firstConnector.Y;

            if (firstX == secondX && firstY == secondY)
            {
                return pointList;
            }

            Point lastPoint = secondConnector;

            pointList.Add(firstConnector);

            if (secondX > firstX && secondY > firstY)
            {
                // 往右下方拖动
                Console.WriteLine("往右下方拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 左边的点，往右下方拖动
                            pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));
                            pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY));

                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 上边的点，往右下方拖动
                            pointList.Add(new Point(firstX, firstY - PadContext.MinimalMargin));
                            pointList.Add(new Point(secondX, firstY - PadContext.MinimalMargin));
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 右边的点，往右下方拖动
                            pointList.Add(new Point(secondX, firstY));
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 下边的点，往右下方拖动
                            pointList.Add(new Point(firstX, secondY));
                            break;
                        }
                }
            }
            else if (secondX > firstX && secondY < firstY)
            {
                // 往右上方拖动
                Console.WriteLine("往右上方拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 左边的点，往右上方拖动
                            pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));
                            pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY));
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 上边的点，往右上方拖动
                            pointList.Add(new Point(firstX, secondY));
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 右边的点，往右上方拖动
                            pointList.Add(new Point(secondX, firstY));
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 下边的点，往右上方拖动
                            pointList.Add(new Point(firstX, firstY + PadContext.MinimalMargin));
                            pointList.Add(new Point(secondX, firstY + PadContext.MinimalMargin));
                            break;
                        }
                }
            }
            else if (secondX < firstX && secondY > firstY)
            {
                // 往左下方拖动
                Console.WriteLine("往左下方拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 左边的点，往左下方拖动
                            pointList.Add(new Point(secondX, firstConnector.Y));
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 上边的点，往左下方拖动
                            pointList.Add(new Point(firstX, firstY - PadContext.MinimalMargin));
                            pointList.Add(new Point(secondX, firstY - PadContext.MinimalMargin));
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 右边的点，往左下方拖动
                            pointList.Add(new Point(firstX + PadContext.MinimalMargin, firstY));
                            pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY));
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 下边的点，往左下方拖动
                            pointList.Add(new Point(firstX, secondY));
                            break;
                        }
                }
            }
            else if (secondX < firstX && secondY < firstY)
            {
                // 往左上方拖动
                Console.WriteLine("往左上方拖动");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 左边的点，往左上方拖动
                            pointList.Add(new Point(secondX, firstConnector.Y));
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 上边的点，往左上方拖动
                            pointList.Add(new Point(firstX, secondY));
                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 右边的点，往左上方拖动
                            pointList.Add(new Point(firstX + PadContext.MinimalMargin, firstY));
                            pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY));
                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 下边的点，往左上方拖动
                            pointList.Add(new Point(firstX, firstY + PadContext.MinimalMargin));
                            pointList.Add(new Point(secondX, firstY + PadContext.MinimalMargin));
                            break;
                        }
                }
            }
            else if (secondX > firstX && secondY == firstY)
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
            else if (secondX < firstX && secondY == firstY)
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
            else if (secondX == firstX && secondY > firstY)
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
            else if (secondX == firstX && secondY < firstY)
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

