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
        /// 计算两个点之间的连接路径
        /// 
        /// 规律：
        /// 上下左右四个连接点，每个连接点都可以往8个方向连接，一共是4 * 8种连接方式
        /// 
        /// 有了这个规律，只要把所有可能的连接方向枚举出来，再一个个实现连接方式就好了
        /// 
        /// 旋转后的连接点连线方式：
        /// 先计算没旋转的连接点列表，然后：
        /// 以图形的边界框的中点为中心，把某个连接点旋转X度，就是新的连接点的位置。
        /// 用矩阵可以实现把某个点旋转X度
        /// </summary>
        /// <param name="firstConnector">第一个连接点</param>
        /// <param name="firstLocation">第一个连接点的位置</param>
        /// <param name="secondConnector">第二个连接点</param>
        /// <returns></returns>
        public static List<Point> MakeConnectionPath(Point firstConnector, ConnectionLocations firstLocation, Point secondConnector)
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
        /// 计算两个已经连接起来的图形之间的连接路径
        /// 
        /// 规律：
        /// 上下左右四个连接点，每个连接点都可以往8个方向连接（左上右下，左上，右上，左下，右下）
        /// 8个方向里，每个方向都可以与第二个图形的4个连接点（上下左右）连接，一共是4 * 8 * 4种连接方式
        /// 
        /// 先判断第二个点在第一个点的哪个方向（一共可以位于8个方向，上下左右，左上，右上，左下，右下）
        /// 再判断第一个点位于图形的哪个位置和第二个点位于图形的哪个位置，这样就确定下来了连接路径
        /// 
        /// 有了这个规律，只要把所有可能的连接方向枚举出来，再一个个实现连接方式就好了
        /// </summary>
        /// <param name="firstConnector"></param>
        /// <param name="firstLocation"></param>
        /// <param name="secondConnector"></param>
        /// <param name="secondLocation"></param>
        /// <returns></returns>
        public static List<Point> MakeConnectionPath(Point firstConnector, ConnectionLocations firstLocation, Point secondConnector, ConnectionLocations secondLocation)
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

            pointList.Add(firstConnector);

            if (secondX > firstX && secondY > firstY)
            {
                // 第二个点在第一个点的右下方
                Console.WriteLine("左上角 - 右下角");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 第一个点在图形的左边
                            // 路径算法：Documents/连接路径规划/连接路径在右下方_连接点在左边.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));
                                        double y = firstY + Math.Ceiling((secondY - firstY) / 2);
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, y));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, y));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 第一个点在图形的上边
                            // 路径算法：Documents/连接路径规划/连接路径在右下方_连接点在上边.png

                            pointList.Add(new Point(firstX, firstY - PadContext.MinimalMargin));

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        double x = firstX + Math.Ceiling((secondX - firstX) / 2);
                                        pointList.Add(new Point(x, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(x, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(secondX, firstY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 第一个点在图形的右边
                            // 路径算法：Documents/连接路径规划/连接路径在右下方_连接点在右边.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        double x = firstX + Math.Ceiling((secondX - firstX) / 2);
                                        pointList.Add(new Point(x, firstY));
                                        pointList.Add(new Point(x, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(secondX, firstY));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 第一个点在图形的下面
                            // 路径算法：Documents/连接路径规划/连接路径在右下方_连接点在下面.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Top:
                                    {
                                        // 第二个点在图形的上面
                                        double y = firstY + Math.Ceiling((secondY - firstY) / 2);
                                        pointList.Add(new Point(firstX, y));
                                        pointList.Add(new Point(secondX, y));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        // 第二个图形的连接点在左边
                                        pointList.Add(new Point(firstX, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        // 第二个图形的连接点在右边
                                        pointList.Add(new Point(firstX, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Bottom:
                                    {
                                        // 第二个图形的连接点在下面
                                        pointList.Add(new Point(firstX, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }
            else if (secondX > firstX && secondY < firstY)
            {
                // 第二个点在第一个点的右上方
                Console.WriteLine("左下角 - 右上角");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 第一个点在图形的左边
                            // 路径算法：Documents/连接路径规划/1-7.png

                            pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        double y = secondY + Math.Ceiling((firstY - secondY) / 2);
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, y));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, y));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 第一个点在图形的上面
                            // 路径算法：Documents/连接路径规划/1-8.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        double y = secondY + Math.Ceiling((firstY - secondY) / 2);
                                        pointList.Add(new Point(firstX, y));
                                        pointList.Add(new Point(secondX, y));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(firstX, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(firstX, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(firstX, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 第一个点在图形的右边
                            // 路径算法：Documents/连接路径规划/1-9.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(secondX, firstY));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        double x = firstX + Math.Ceiling((secondX - firstX) / 2);
                                        pointList.Add(new Point(x, firstY));
                                        pointList.Add(new Point(x, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 第一个点在图形的下面
                            // 路径算法：Documents/连接路径规划/1-4.png

                            pointList.Add(new Point(firstX, firstY + PadContext.MinimalMargin));

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(secondX, firstY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        double x = firstX + Math.Ceiling((secondX - firstX) / 2);
                                        pointList.Add(new Point(x, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(x, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }
            else if (secondX < firstX && secondY > firstY)
            {
                // 第二个点在第一个点的左下方
                Console.WriteLine("右上角 - 左下角");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 第一个点在图形的左边
                            // 路径算法：Documents/连接路径规划/1-10.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        double x = secondX + Math.Ceiling((firstX - secondX) / 2);
                                        pointList.Add(new Point(x, firstY));
                                        pointList.Add(new Point(x, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(secondX, firstY));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 第一个点在图形的上边
                            // 路径算法：Documents/连接路径规划/1-11.png

                            pointList.Add(new Point(firstX, firstY - PadContext.MinimalMargin));

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        double x = secondX + Math.Ceiling((firstX - secondX) / 2);
                                        pointList.Add(new Point(x, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(x, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(secondX, firstY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 第一个点在图形的右边
                            // 路径算法：Documents/连接路径规划/1-12.png

                            pointList.Add(new Point(firstX + PadContext.MinimalMargin, firstY));

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        double y = firstY + Math.Ceiling((secondY - firstY) / 2);
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, y));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, y));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 第一个点在图形的下面
                            // 路径算法：Documents/连接路径规划/1-6.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(firstX, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(firstX, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(firstX, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        double y = firstY + Math.Ceiling((secondY - firstY) / 2);
                                        pointList.Add(new Point(firstX, y));
                                        pointList.Add(new Point(secondX, y));
                                        break;
                                    }
                            }

                            break;
                        }
                }
            }
            else if (secondX < firstX && secondY < firstY)
            {
                // 第二个点在第一个点的左上方
                Console.WriteLine("右下角 - 左上角");

                switch (firstLocation)
                {
                    case ConnectionLocations.Left:
                        {
                            // 第一个点在图形的左边
                            // 路径算法：Documents/连接路径规划/1-14.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(secondX, firstY));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        double x = secondX + Math.Ceiling((firstX - secondX) / 2);
                                        pointList.Add(new Point(x, firstY));
                                        pointList.Add(new Point(x, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, firstY));
                                        pointList.Add(new Point(firstX - PadContext.MinimalMargin, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }
                            
                            break;
                        }

                    case ConnectionLocations.Top:
                        {
                            // 第一个点在图形的上边
                            // 路径算法：Documents/连接路径规划/1-15.png

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        double y = secondY + Math.Ceiling((firstY - secondY) / 2);
                                        pointList.Add(new Point(firstX, y));
                                        pointList.Add(new Point(secondX, y));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(firstX, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, firstY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(firstX, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(firstX, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Right:
                        {
                            // 第一个点在图形的右边
                            // 路径算法：Documents/连接路径规划/1-13.png

                            pointList.Add(new Point(firstX + PadContext.MinimalMargin, firstY));

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        double y = secondY + Math.Ceiling((firstY - secondY) / 2);
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, y));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, y));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        pointList.Add(new Point(firstX + PadContext.MinimalMargin, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

                            break;
                        }

                    case ConnectionLocations.Bottom:
                        {
                            // 第一个点在图形的下边
                            // 路径算法：Documents/连接路径规划/1-5.png

                            pointList.Add(new Point(firstX, firstY + PadContext.MinimalMargin));

                            switch (secondLocation)
                            {
                                case ConnectionLocations.Bottom:
                                    {
                                        pointList.Add(new Point(secondX, firstY + PadContext.MinimalMargin));
                                        break;
                                    }

                                case ConnectionLocations.Left:
                                    {
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX - PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Right:
                                    {
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX + PadContext.MinimalMargin, secondY));
                                        break;
                                    }

                                case ConnectionLocations.Top:
                                    {
                                        double x = secondX + Math.Ceiling((firstX - secondX) / 2);
                                        pointList.Add(new Point(x, firstY + PadContext.MinimalMargin));
                                        pointList.Add(new Point(x, secondY - PadContext.MinimalMargin));
                                        pointList.Add(new Point(secondX, secondY - PadContext.MinimalMargin));
                                        break;
                                    }
                            }

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

            pointList.Add(secondConnector);

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

