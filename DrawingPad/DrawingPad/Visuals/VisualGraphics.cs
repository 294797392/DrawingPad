using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawingPad.Visuals
{
    public abstract class VisualGraphics : DrawingVisual
    {
        #region 实例变量

        private Typeface typeface;

        /// <summary>
        /// 文本边距
        /// </summary>
        private const int TextMargin = 10;

        #endregion

        #region 属性

        /// <summary>
        /// 图形的ID
        /// </summary>
        public string ID { get { return this.Graphics.ID; } }

        /// <summary>
        /// 保存图形数据
        /// </summary>
        public GraphicsBase Graphics { get; private set; }

        /// <summary>
        /// 获取图形类型
        /// </summary>
        public GraphicsType Type { get { return this.Graphics.Type; } }

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
        public int ConnectionHandles { get { return this.Graphics.ConnectionHandles; } }

        /// <summary>
        /// 获取矩形拖拽点的数量
        /// </summary>
        public int ResizeHandles { get { return this.Graphics.ResizeHandles; } }

        /// <summary>
        /// 图形
        /// </summary>
        public abstract Geometry Geometry { get; }

        #endregion

        #region 构造方法

        public VisualGraphics(GraphicsBase graphics)
        {
            this.Name = Guid.NewGuid().ToString();
            this.Graphics = graphics;
        }

        #endregion

        #region 抽象函数

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
                for (int i = 0; i < this.ConnectionHandles; i++)
                {
                    Point center = this.GetConnectorPoint(i);

                    dc.DrawEllipse(PadContext.TrackerBackground, PadContext.TrackerPen, center, PadContext.CircleTrackerRadius, PadContext.CircleTrackerRadius);
                }

                for (int i = 0; i < this.ResizeHandles; i++)
                {
                    Rect rect = this.GetResizeHandleBounds(i);

                    dc.DrawRectangle(PadContext.TrackerBackground, PadContext.TrackerPen, rect);
                }
            }
            else
            {

            }

            // 如果文本不为空,那么渲染文本
            if (!string.IsNullOrEmpty(this.Graphics.TextProperties.Text))
            {
                TextProperties textProperty = this.Graphics.TextProperties;

                if (this.typeface == null)
                {
                    this.typeface = new Typeface(new FontFamily("宋体"), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
                }

                Rect bounds = this.GetTextBounds();

                FormattedText text = new FormattedText(textProperty.Text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.typeface, 12, Brushes.Black);
                text.MaxTextWidth = bounds.Width;
                text.MaxTextHeight = bounds.Height;
                dc.DrawText(text, bounds.Location);
            }

            dc.Close();
        }

        /// <summary>
        /// 获取文本边界框
        /// </summary>
        /// <returns></returns>
        public Rect GetTextBounds()
        {
            Rect bounds = this.GetBounds();
            bounds.Inflate(-TextMargin, -TextMargin);
            return bounds;
        }

        /// <summary>
        /// 获取该图形的边界框
        /// </summary>
        /// <returns></returns>
        public Rect GetBounds()
        {
            return this.Graphics.GetBounds();
        }

        /// <summary>
        /// 图形是否包含一点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual bool Contains(Point p)
        {
            return this.GetBounds().Contains(p);
        }

        /// <summary>
        /// 对图形进行平移
        /// </summary>
        /// <param name="offsetX">平移的X偏移量</param>
        /// <param name="offsetY">平移的Y偏移量</param>
        public void Translate(double offsetX, double offsetY)
        {
            this.Graphics.Translate(offsetX, offsetY);
            this.Render();
        }

        /// <summary>
        /// 对图形做缩放操作
        /// </summary>
        /// <param name="location">缩放的点的坐标</param>
        /// <param name="newPos">新的坐标</param>
        public virtual void Resize(ResizeLocations location, Point resizePos, Point newPos)
        {
            this.Graphics.Resize(location, resizePos, newPos);
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

        public virtual Point GetConnectorPoint(int index)
        {
            return this.Graphics.GetConnectionPoint(index);
        }

        public virtual ConnectionLocations GetConnectorLocation(int handleIndex)
        {
            return this.Graphics.GetConnectorLocation(handleIndex);
        }

        public virtual Point GetResizeHandle(int index)
        {
            return this.Graphics.GetResizeHandle(index);
        }

        public Rect GetConnectorBounds(int index)
        {
            return this.Graphics.GetConnectorBounds(index);
        }

        public Rect GetResizeHandleBounds(int index)
        {
            return this.Graphics.GetResizeHandleBounds(index);
        }

        #endregion
    }
}
