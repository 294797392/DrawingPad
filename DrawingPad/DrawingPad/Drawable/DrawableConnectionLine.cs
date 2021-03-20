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
    public class DrawableConnectionLine : DrawableVisual
    {
        #region 实例变量

        private GraphicsConnectionLine graphics;

        #endregion

        #region 属性

        public override int CircleHandles { get; protected set; }
        public override int RectangleHandles { get; protected set; }

        #endregion

        #region 构造方法

        public DrawableConnectionLine(GraphicsBase graphics) : base(graphics)
        {
            this.graphics = graphics as GraphicsConnectionLine;
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

        public override Rect GetBounds()
        {
            throw new NotImplementedException();
        }

        public override bool Contains(Point p)
        {
            return false;
        }

        #endregion

        /// <summary>
        /// 获取两点之间的连接点列表
        /// </summary>
        /// <param name="startPoint">起始连接点的位置</param>
        /// <param name="pointPos">起始点相对于中点的位置</param>
        /// <param name="cursorPos">目标点</param>
        /// <returns></returns>
        public void Update(DrawableVisual startVisual, DrawableVisual visualHit, Point cursorPos)
        {
            Point startPoint = this.graphics.ConnectionPoint;

            double cursorX = cursorPos.X;
            double cursorY = cursorPos.Y;
            double startX = startPoint.X;
            double startY = startPoint.Y;

            if (startX == cursorX && startY == cursorY)
            {
                return;
            }

            PointPositions startPointPos = GraphicsUtility.GetPointPosition(startVisual, startPoint);

            Rect startVisualBounds = startVisual.GetBounds();

            List<Point> pointList = new List<Point>();

            if (cursorX > startX && cursorY > startY)
            {
                // 往右下方拖动
                Console.WriteLine("往右下方拖动");

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            // 左边的点，往右下方拖动
                            pointList.Add(startPoint);
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));
                            if (startVisualBounds.Contains(cursorPos))
                            {
                                // 鼠标在图形里面
                                pointList.Add(new Point(startX - PadContext.MinimalMargin, startY + PadContext.MinimalMargin));
                                pointList.Add(new Point(cursorX, startY + PadContext.MinimalMargin));
                            }
                            else
                            {
                                // 鼠标在图形外面
                                pointList.Add(new Point(startVisualBounds.BottomLeft.X - PadContext.MinimalMargin, cursorY));
                                pointList.Add(new Point(cursorX, cursorY));
                            }
                            break;
                        }

                    case PointPositions.CenterTop:
                        {
                            // 上边的点，往右下方拖动
                            pointList.Add(startPoint);
                            pointList.Add(new Point(startX, startY - PadContext.MinimalMargin));
                            if (startVisualBounds.Contains(cursorPos))
                            {
                                // 鼠标在图形里面
                                pointList.Add(new Point(cursorX, startY - PadContext.MinimalMargin));
                                pointList.Add(cursorPos);
                            }
                            else
                            {
                                // 鼠标在图形外面
                            }
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
            else if (cursorX > startX && cursorY < startY)
            {
                // 往右上方拖动
                Console.WriteLine("往右上方拖动");

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            // 左边的点，往右上方拖动
                            pointList.Add(startPoint);
                            pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));
                            if (startVisualBounds.Contains(cursorPos))
                            {
                                // 鼠标在图形里面
                                pointList.Add(new Point(startX - PadContext.MinimalMargin, startY + PadContext.MinimalMargin));
                                pointList.Add(new Point(cursorX, startY + PadContext.MinimalMargin));
                            }
                            else
                            {
                                // 鼠标在图形外面
                                pointList.Add(new Point(startVisualBounds.BottomLeft.X - PadContext.MinimalMargin, cursorY));
                                pointList.Add(new Point(cursorX, cursorY));
                            }
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
            else if (cursorX < startX && cursorY > startY)
            {
                // 往左下方拖动
                Console.WriteLine("往左下方拖动");

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            // 左边的点，往左下方拖动
                            pointList.Add(startPoint);
                            pointList.Add(new Point(cursorX, startPoint.Y));
                            pointList.Add(cursorPos);
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
            else if (cursorX < startX && cursorY < startY)
            {
                // 往左上方拖动
                Console.WriteLine("往左上方拖动");

                switch (startPointPos)
                {
                    case PointPositions.CenterLeft:
                        {
                            // 左边的点，往左上方拖动
                            pointList.Add(startPoint);
                            pointList.Add(new Point(cursorX, startPoint.Y));
                            pointList.Add(cursorPos);
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

            DrawingContext dc = this.RenderOpen();

            int count = pointList.Count;

            for (int i = 0; i < count - 1; i++)
            {
                dc.DrawLine(PadContext.LinePen, pointList[i], pointList[i + 1]);
            }

            dc.Close();
        }
    }
}
