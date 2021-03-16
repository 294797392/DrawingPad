using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Drawable
{
    public static class DrawableVisualFactory
    {
        public static DrawableVisual Create(GraphicsBase graphics)
        {
            switch (graphics.Type)
            {
                case GraphicsType.Rectangle:
                    {
                        return new DrawableRectangle(graphics);
                    }

                case GraphicsType.ConnectionLine:
                    {
                        return new DrawableConnectionLine(graphics);
                    }

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
