using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawingPad.Drawable
{
    public class DrawableNull : DrawableVisual
    {
        public static readonly DrawableNull Empty = new DrawableNull();

        private static readonly PointCollection ConnectionPoints = new PointCollection();

        public DrawableNull() : base(null)
        {
            
        }

        public override PointCollection GetConnectionPoints()
        {
            return ConnectionPoints;
        }

        protected override void RenderCore(DrawingContext dc)
        {
        }
    }
}
