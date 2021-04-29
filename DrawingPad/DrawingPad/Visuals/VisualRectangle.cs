using DrawingPad.Graphics;
using SciencePad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DrawingPad.Visuals
{
    public class VisualRectangle : VisualGraphics
    {
        #region 常量

        private const int DefaultBorderWidth = 2;

        #endregion

        #region 实例变量

        private Brush brush = PadBrushes.Background;
        private Pen borderPen = Pens.Black;

        private GraphicsRectangle graphicsRect;

        private RectangleGeometry geometry;

        #endregion

        #region 属性

        public override Geometry Geometry { get { return this.geometry; } }

        #endregion

        #region 构造方法

        public VisualRectangle(GraphicsBase graphics) :
            base(graphics)
        {
            this.graphicsRect = graphics as GraphicsRectangle;
        }

        #endregion

        #region DrawableVisual

        protected override void RenderCore(DrawingContext dc)
        {
            if (this.geometry == null)
            {
                this.geometry = new RectangleGeometry();
            }

            this.geometry.Rect = this.graphicsRect.MakeRect();

            dc.DrawGeometry(PadContext.DefaultFillBrush, PadContext.DefaultPen, this.geometry);

            //dc.DrawRectangle(this.brush, this.borderPen, this.graphicsRect.MakeRect());
        }

        #endregion

        #region 实例方法

        #endregion
    }
}
