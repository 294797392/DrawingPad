using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    public class GraphicsRectangle : GraphicsBase
    {
        public override GraphicsType Type { get { return GraphicsType.Rectangle; } }

        public double Point1X { get; set; }

        public double Point1Y { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public Rect MakeRect()
        {
            return new Rect()
            {
                X = this.Point1X,
                Y = this.Point1Y,
                Height = this.Height,
                Width = this.Width
            };
        }
    }
}
