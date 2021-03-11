using DrawingPad.Graphics;
using SciencePad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrawingPad.Drawable
{
    public class DrawableRectangle : DrawableVisual
    {
        #region 常量

        private const int DefaultBorderWidth = 2;

        #endregion

        #region 实例变量

        private Brush brush = PadBrushes.Background;
        private Pen borderPen = Pens.Black;

        private GraphicsRectangle graphicsRect;

        #endregion

        #region 构造方法

        public DrawableRectangle(GraphicsBase graphics) :
            base(graphics)
        {
            this.graphicsRect = graphics as GraphicsRectangle;
        }

        #endregion

        protected override void RenderCore(DrawingContext dc)
        {
            dc.DrawRectangle(this.brush, this.borderPen, this.graphicsRect.MakeRect());
        }

        public override PointCollection GetConnectionPoints()
        {
            throw new NotImplementedException();
        }

        #region 实例方法

        #endregion
    }
}
