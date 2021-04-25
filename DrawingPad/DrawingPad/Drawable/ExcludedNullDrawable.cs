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
    public class ExcludedNullDrawable : DrawableVisual
    {
        public override Geometry Geometry => throw new NotImplementedException();

        public ExcludedNullDrawable(GraphicsBase graphics) : 
            base(graphics)
        {
        }

        public override bool Contains(Point p)
        {
            throw new NotImplementedException();
        }

        protected override void RenderCore(DrawingContext dc)
        {
            throw new NotImplementedException();
        }
    }
}
