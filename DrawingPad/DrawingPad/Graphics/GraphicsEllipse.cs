using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    public class GraphicsEllipse : GraphicsRectangle
    {
        #region 属性

        public override GraphicsType Type { get { return GraphicsType.Ellipse; } }

        public double RadiusX { get { return this.Width / 2; } }

        public double RadiusY { get { return this.Height / 2; } }

        public double CenterX { get { return this.Point1X + this.RadiusX; } }

        public double CenterY { get { return this.Point1Y + this.RadiusY; } }

        #endregion

        #region GraphicsBase

        public override Rect GetBounds()
        {
            return new Rect() 
            {
                X = this.Point1X,
                Y = this.Point1Y,
                Height = this.Height,
                Width = this.Width
            };
        }

        #endregion
    }
}
