using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawingPad.Drawable
{
    /// <summary>
    /// 用来画连接点的东西
    /// </summary>
    public class DrawableConnectionPoint : DrawingVisual
    {
        public void Render(Point center)
        {
            DrawingContext dc = this.RenderOpen();

            dc.DrawEllipse(PadBrushes.ConnectionPointBackground, Pens.ConnectionPointBorder, center, 5, 5);

            dc.Close();
        }
    }
}
