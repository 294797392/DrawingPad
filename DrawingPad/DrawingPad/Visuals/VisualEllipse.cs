using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawingPad.Visuals
{
    public class VisualEllipse : VisualGraphics
    {
        #region 实例变量

        private EllipseGeometry geometry;
        private GraphicsEllipse graphicsEllipse;

        #endregion

        #region 属性

        public override Geometry Geometry { get { return this.geometry; } }

        #endregion

        #region 构造方法

        public VisualEllipse(GraphicsBase graphics) : base(graphics)
        {
            this.graphicsEllipse = graphics as GraphicsEllipse;
        }

        #endregion

        #region VisualGraphics

        protected override void RenderCore(DrawingContext dc)
        {
            if (this.geometry == null)
            {
                this.geometry = new EllipseGeometry();
            }

            this.geometry.RadiusX = this.graphicsEllipse.RadiusX;
            this.geometry.RadiusY = this.graphicsEllipse.RadiusY;
            this.geometry.Center = new System.Windows.Point(this.graphicsEllipse.CenterX, this.graphicsEllipse.CenterY);

            dc.DrawGeometry(PadContext.DefaultFillBrush, PadContext.DefaultPen, this.Geometry);
        }

        #endregion
    }
}
