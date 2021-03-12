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
        public static readonly DrawableNull Null = new DrawableNull();

        #region 实例变量

        #endregion

        #region 属性

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

        #endregion

        #region 构造方法

        public DrawableVisual(GraphicsBase graphics)
        {
            this.Name = Guid.NewGuid().ToString();
        }

        #endregion

        #region 抽象函数

        /// <summary>
        /// 获取当前图形的所有的连接点
        /// </summary>
        /// <returns></returns>
        public abstract PointCollection GetCircleTrackers();

        /// <summary>
        /// 获取拖拽点
        /// </summary>
        /// <returns></returns>
        public abstract PointCollection GetRectangleTrackers();

        /// <summary>
        /// 获取旋转点坐标
        /// </summary>
        /// <returns></returns>
        public abstract Point GetRotationPoint();

        protected abstract void RenderCore(DrawingContext dc);

        #endregion

        #region 公开接口

        public void Render()
        {
            DrawingContext dc = this.RenderOpen();

            this.RenderCore(dc);

            if (this.IsMouseHover || this.IsSelected)
            {
                PointCollection points = this.GetCircleTrackers();

                foreach (Point center in points)
                {
                    dc.DrawEllipse(PadContext.TrackerBackground, PadContext.TrackerPen, center, PadContext.TrackerSize, PadContext.TrackerSize);
                }
            }

            if (this.IsSelected)
            {
                PointCollection points = this.GetRectangleTrackers();

                foreach (Point center in points)
                {
                    Rect rect = new Rect()
                    {
                        Height = PadContext.RectangleTrackerSize,
                        Width = PadContext.RectangleTrackerSize,
                        Location = new Point(center.X - PadContext.RectangleTrackerSize / 2, center.Y - PadContext.RectangleTrackerSize / 2)
                    };

                    dc.DrawRectangle(PadContext.TrackerBackground, PadContext.TrackerPen, rect);
                }
            }

            dc.Close();
        }

        #endregion
    }
}
