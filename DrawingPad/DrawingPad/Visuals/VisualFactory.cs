using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Visuals
{
    public static class VisualFactory
    {
        public static VisualGraphics Create(GraphicsBase graphics)
        {
            switch (graphics.Type)
            {
                case GraphicsType.Rectangle:
                    {
                        return new VisualRectangle(graphics);
                    }

                case GraphicsType.Polyline:
                    {
                        return new VisualPolyline(graphics);
                    }

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
