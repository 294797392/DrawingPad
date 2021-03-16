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
    public abstract class DrawableVisual : DrawingVisual
    {
        #region 实例变量

        #endregion

        #region 属性

        public GraphicsBase Graphics { get; private set; }

        /// <summary>
        /// 图形的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 鼠标是否移动上去了
        /// </summary>
        public bool IsMouseHover { get; set; }

        /// <summary>
        /// 是否是选中状态
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// 获取圆形连接点的数量
        /// </summary>
        public abstract int CircleHandles { get; protected set; }

        /// <summary>
        /// 获取矩形拖拽点的数量
        /// </summary>
        public abstract int RectangleHandles { get; protected set; }

        #endregion

        #region 构造方法

        public DrawableVisual(GraphicsBase graphics)
        {
            this.Name = Guid.NewGuid().ToString();
            this.Graphics = graphics;
        }

        #endregion

        #region 抽象函数

        /// <summary>
        /// 获取旋转点坐标
        /// </summary>
        /// <returns></returns>
        public abstract Point GetRotationHandle();

        public abstract Point GetCircleHandle(int num);

        public abstract Point GetRectangleHandle(int num);

        /// <summary>
        /// 获取该图形的边界框
        /// </summary>
        /// <returns></returns>
        public abstract Rect GetBounds();

        /// <summary>
        /// 图形是否包含一点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public abstract bool Contains(Point p);

        protected abstract void RenderCore(DrawingContext dc);

        #endregion

        #region 公开接口

        public void Render()
        {
            DrawingContext dc = this.RenderOpen();

            this.RenderCore(dc);

            if (this.IsMouseHover || this.IsSelected)
            {
                for (int i = 0; i < this.CircleHandles; i++)
                {
                    Point center = this.GetCircleHandle(i);

                    dc.DrawEllipse(PadContext.TrackerBackground, PadContext.TrackerPen, center, PadContext.CircleTrackerRadius, PadContext.CircleTrackerRadius);
                }
            }
            else
            {
            }

            if (this.IsSelected)
            {
                for (int i = 0; i < this.RectangleHandles; i++)
                {
                    Rect rect = this.GetRectangleHandleBounds(i);

                    dc.DrawRectangle(PadContext.TrackerBackground, PadContext.TrackerPen, rect);
                }
            }
            else
            {
            }

            dc.Close();
        }

        /// <summary>
        /// 获取圆形连接点的边界框
        /// </summary>
        /// <returns></returns>
        public Rect GetCircleHandleBounds(int num)
        {
            Point center = this.GetCircleHandle(num);

            return new Rect()
            {
                Height = PadContext.CircleTrackerRadius * 2,
                Width = PadContext.CircleTrackerRadius * 2,
                Location = new Point(center.X - PadContext.CircleTrackerRadius, center.Y - PadContext.CircleTrackerRadius)
            };
        }

        /// <summary>
        /// 获取矩形拖拽点的边界框
        /// </summary>
        /// <param name="numHandle"></param>
        /// <returns></returns>
        public Rect GetRectangleHandleBounds(int num)
        {
            Point center = this.GetRectangleHandle(num);

            return new Rect()
            {
                Height = PadContext.RectangleTrackerSize,
                Width = PadContext.RectangleTrackerSize,
                Location = new Point(center.X - PadContext.RectangleTrackerSize / 2, center.Y - PadContext.RectangleTrackerSize / 2)
            };
        }

        public void UpdatePosition(double offsetX, double offsetY)
        {
            this.Graphics.UpdatePosition(offsetX, offsetY);
        }

        /// <summary>
        /// 获取两点之间的连接点列表
        /// </summary>
        /// <param name="startPoint">起始连接点的位置</param>
        /// <param name="pointPos">起始点相对于中点的位置</param>
        /// <param name="targetPoint">目标点</param>
        /// <returns></returns>
        public virtual List<Point> GetConnectionPoints(Point startPoint, PointPositions startPointPos, Point targetPoint)
        {
            //double cursorX = targetPoint.X;
            //double cursorY = targetPoint.Y;
            //double startX = startPoint.X;
            //double startY = startPoint.Y;

            //if (startX == cursorX && startY == cursorY)
            //{
            //    return null;
            //}

            //Rect startVisualBounds = this.GetBounds(); startVisual.GetBounds();

            //List<Point> pointList = new List<Point>();

            //if (cursorX > startX && cursorY > startY)
            //{
            //    // 往右下方拖动
            //    Console.WriteLine("往右下方拖动");

            //    switch (this.graphics.StartPointPosition)
            //    {
            //        case PointPositions.CenterLeft:
            //            {
            //                // 左边的点，往右下方拖动
            //                pointList.Add(startPoint);
            //                pointList.Add(new Point(startX - PadContext.MinimalMargin, startY));
            //                if (startVisual.Contains(cursorPosition))
            //                {
            //                    // 鼠标在图形里面
            //                    pointList.Add(new Point(startVisualBounds.BottomLeft.X - PadContext.MinimalMargin, startVisualBounds.BottomRight.Y + PadContext.MinimalMargin));
            //                    pointList.Add(new Point(cursorX, startVisualBounds.BottomLeft.Y + PadContext.MinimalMargin));
            //                }
            //                else
            //                {
            //                    // 鼠标在图形外面
            //                    pointList.Add(new Point(startVisualBounds.BottomLeft.X - PadContext.MinimalMargin, cursorY));
            //                    pointList.Add(new Point(cursorX, cursorY));
            //                }
            //                break;
            //            }

            //        case PointPositions.CenterTop:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterRight:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterBottom:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else if (cursorX > startX && cursorY < startY)
            //{
            //    // 往右上方拖动
            //    Console.WriteLine("往右上方拖动");

            //    switch (this.graphics.StartPointPosition)
            //    {
            //        case PointPositions.CenterLeft:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterTop:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterRight:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterBottom:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else if (cursorX < startX && cursorY > startY)
            //{
            //    // 往左下方拖动
            //    Console.WriteLine("往左下方拖动");

            //    switch (this.graphics.StartPointPosition)
            //    {
            //        case PointPositions.CenterLeft:
            //            {
            //                // 左边的点，往左下方拖动
            //                pointList.Add(startPoint);
            //                pointList.Add(new Point(cursorX, startPoint.Y));
            //                pointList.Add(cursorPosition);
            //                break;
            //            }

            //        case PointPositions.CenterTop:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterRight:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterBottom:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else if (cursorX < startX && cursorY < startY)
            //{
            //    // 往左上方拖动
            //    Console.WriteLine("往左上方拖动");

            //    switch (this.graphics.StartPointPosition)
            //    {
            //        case PointPositions.CenterLeft:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterTop:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterRight:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterBottom:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else if (cursorX > startX && cursorY == startY)
            //{
            //    // 往右拖动
            //    Console.WriteLine("往右拖动");

            //    switch (this.graphics.StartPointPosition)
            //    {
            //        case PointPositions.CenterLeft:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterTop:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterRight:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterBottom:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else if (cursorX < startX && cursorY == startY)
            //{
            //    // 往左拖动
            //    Console.WriteLine("往左拖动");

            //    switch (this.graphics.StartPointPosition)
            //    {
            //        case PointPositions.CenterLeft:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterTop:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterRight:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterBottom:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else if (cursorX == startX && cursorY > startY)
            //{
            //    // 往下拖动
            //    Console.WriteLine("往下拖动");

            //    switch (this.graphics.StartPointPosition)
            //    {
            //        case PointPositions.CenterLeft:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterTop:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterRight:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterBottom:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else if (cursorX == startX && cursorY < startY)
            //{
            //    // 往上拖动
            //    Console.WriteLine("往上拖动");

            //    switch (this.graphics.StartPointPosition)
            //    {
            //        case PointPositions.CenterLeft:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterTop:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterRight:
            //            {
            //                break;
            //            }

            //        case PointPositions.CenterBottom:
            //            {
            //                break;
            //            }
            //    }
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}
            throw new NotImplementedException();
        }

        #endregion
    }
}
