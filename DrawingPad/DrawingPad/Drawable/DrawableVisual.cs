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

        protected GraphicsBase graphics;

        #endregion

        #region 属性

        /// <summary>
        /// 图形的名字
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region 构造方法

        public DrawableVisual(GraphicsBase graphics) 
        {
            this.graphics = graphics;
            this.Name = Guid.NewGuid().ToString();
        }

        #endregion

        #region 抽象函数

        /// <summary>
        /// 获取当前图形的所有的连接点
        /// </summary>
        /// <returns></returns>
        public abstract PointCollection GetConnectionPoints();

        protected abstract void RenderCore(DrawingContext dc);

        #endregion

        #region 公开接口

        public void Render()
        {
            DrawingContext dc = this.RenderOpen();

            this.RenderCore(dc);

            dc.Close();
        }

        #endregion
    }
}
