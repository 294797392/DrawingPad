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
    /// <summary>
    /// 折现图形
    /// </summary>
    public class DrawablePolyline : DrawableVisual
    {
        #region 实例变量

        private GraphicsConnectionLine graphics;

        #endregion

        #region 属性

        public override int CircleHandles { get; protected set; }
        public override int RectangleHandles { get; protected set; }
        public override Geometry Geometry { get; }

        #endregion

        #region 构造方法

        public DrawablePolyline(GraphicsBase graphics) : base(graphics)
        {
            this.graphics = graphics as GraphicsConnectionLine;
        }

        #endregion

        #region DrawableVisual

        protected override void RenderCore(DrawingContext dc)
        {
        }

        public override bool Contains(Point p)
        {
            return false;
        }

        #endregion

        #region 公开接口

        /// <summary>
        /// 获取两点之间的连接点列表
        /// </summary>
        /// <param name="startPoint">起始连接点的位置</param>
        /// <param name="pointPos">起始点相对于中点的位置</param>
        /// <param name="cursorPos">目标点</param>
        /// <returns></returns>
        public void Update(List<Point> pointList)
        {
            DrawingContext dc = this.RenderOpen();

            int count = pointList.Count;

            for (int i = 0; i < count - 1; i++)
            {
                dc.DrawLine(PadContext.DefaultPen, pointList[i], pointList[i + 1]);
            }

            dc.Close();
        }

        #endregion
    }
}
