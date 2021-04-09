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

        /// <summary>
        /// 保存图形数据
        /// </summary>
        public GraphicsBase Graphics { get; private set; }

        /// <summary>
        /// 图形的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否画句柄
        /// </summary>
        public bool IsDrawHandle { get; set; }

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

        #region 实例方法

        #endregion

        #region 公开接口

        public void Render()
        {
            DrawingContext dc = this.RenderOpen();

            this.RenderCore(dc);

            if (this.IsDrawHandle || this.IsSelected)
            {
                for (int i = 0; i < this.CircleHandles; i++)
                {
                    Point center = this.GetConnectionHandle(i);

                    dc.DrawEllipse(PadContext.TrackerBackground, PadContext.TrackerPen, center, PadContext.CircleTrackerRadius, PadContext.CircleTrackerRadius);
                }

                for (int i = 0; i < this.RectangleHandles; i++)
                {
                    Rect rect = this.GetResizeHandleBounds(i);

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
        public Rect GetConnectionHandleBounds(int num)
        {
            Point center = this.GetConnectionHandle(num);

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
        public Rect GetResizeHandleBounds(int num)
        {
            Point center = this.GetResizeHandle(num);

            return new Rect()
            {
                Height = PadContext.RectangleTrackerSize,
                Width = PadContext.RectangleTrackerSize,
                Location = new Point(center.X - PadContext.RectangleTrackerSize / 2, center.Y - PadContext.RectangleTrackerSize / 2)
            };
        }

        public void Translate(double offsetX, double offsetY)
        {
            this.Graphics.Translate(offsetX, offsetY);
            this.Render();
        }

        /// <summary>
        /// 对图形做缩放操作
        /// </summary>
        /// <param name="vertex">调整大小的顶点位置</param>
        public virtual void Resize(GraphicsVertexPosition vertex, Point oldPos, Point newPos)
        {
            this.Graphics.Resize(vertex, oldPos, newPos);
            this.Render();
        }

        /// <summary>
        /// 获取旋转点坐标
        /// </summary>
        /// <returns></returns>
        public virtual Point GetRotationHandle()
        {
            return this.Graphics.GetRotationHandle();
        }

        public virtual Point GetConnectionHandle(int index)
        {
            return this.Graphics.GetConnectionHandle(index);
        }

        public virtual Point GetResizeHandle(int index)
        {
            return this.Graphics.GetResizeHandle(index);
        }

        #endregion
    }
}
