using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Drawable
{
    public static class DrawableVisualUtility
    {
        public static List<Point> GetConnectionPoints(DrawableVisual startVisual, Point startPoint, DrawableVisual visualHit, Point cursorPos)
        {
            double cursorX = cursorPos.X;
            double cursorY = cursorPos.Y;
            double startX = startPoint.X;
            double startY = startPoint.Y;

            if (startX == cursorX && startY == cursorY)
            {
                return null;
            }

            PointPositions startPointPos = GraphicsUtility.GetPointPosition(startVisual, startPoint);

            Rect startVisualBounds = startVisual.GetBounds();

            List<Point> pointList = new List<Point>();

            pointList.Add(startPoint);

            if (cursorX > startX && cursorY > startY)
            {
                // 往右下方拖动
                Console.WriteLine("往右下方拖动");

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            // 左边的点，往右下方拖动
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            // 上边的点，往右下方拖动
                            pointList.Add(new Point(startX, startY - PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY - PadContext.MinimalMargin));
                            break;
                        }

                    case PointPositions.CenterRight:
                        {
                            // 右边的点，往右下方拖动
                            pointList.Add(new Point(cursorX, startY));
                            break;
                        }

                    case PointPositions.CenterBottom:
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

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            // 左边的点，往右上方拖动
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            // 上边的点，往右上方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }

                    case PointPositions.CenterRight:
                        {
                            // 右边的点，往右上方拖动
                            pointList.Add(new Point(cursorX, startY));
                            break;
                        }

                    case PointPositions.CenterBottom:
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

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            // 左边的点，往左下方拖动
                            pointList.Add(new Point(cursorX, startPoint.Y));
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            // 上边的点，往左下方拖动
                            pointList.Add(new Point(startX, startY - PadContext.MinimalMargin));
                            pointList.Add(new Point(cursorX, startY - PadContext.MinimalMargin));
                            break;
                        }

                    case PointPositions.CenterRight:
                        {
                            // 右边的点，往左下方拖动
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case PointPositions.CenterBottom:
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

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            // 左边的点，往左上方拖动
                            pointList.Add(new Point(cursorX, startPoint.Y));
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            // 上边的点，往左上方拖动
                            pointList.Add(new Point(startX, cursorY));
                            break;
                        }

                    case PointPositions.CenterRight:
                        {
                            // 右边的点，往左上方拖动
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, startY));
                            pointList.Add(new Point(startX + PadContext.MinimalMargin, cursorY));
                            break;
                        }

                    case PointPositions.CenterBottom:
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

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            break;
                        }

                    case PointPositions.CenterRight:
                        {
                            break;
                        }

                    case PointPositions.CenterBottom:
                        {
                            break;
                        }
                }
            }
            else if (cursorX < startX && cursorY == startY)
            {
                // 往左拖动
                Console.WriteLine("往左拖动");

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            break;
                        }

                    case PointPositions.CenterRight:
                        {
                            break;
                        }

                    case PointPositions.CenterBottom:
                        {
                            break;
                        }
                }
            }
            else if (cursorX == startX && cursorY > startY)
            {
                // 往下拖动
                Console.WriteLine("往下拖动");

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            break;
                        }

                    case PointPositions.CenterRight:
                        {
                            break;
                        }

                    case PointPositions.CenterBottom:
                        {
                            break;
                        }
                }
            }
            else if (cursorX == startX && cursorY < startY)
            {
                // 往上拖动
                Console.WriteLine("往上拖动");

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            break;
                        }

                    case PointPositions.CenterRight:
                        {
                            break;
                        }

                    case PointPositions.CenterBottom:
                        {
                            break;
                        }
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            pointList.Add(cursorPos);

            return pointList;
        }
    }
}
