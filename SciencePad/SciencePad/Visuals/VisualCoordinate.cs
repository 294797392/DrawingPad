using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SciencePad.Visuals
{
    /// <summary>
    /// 坐标系
    /// </summary>
    public class VisualCoordinate : VisualGeometry
    {
        public Point XAxisPoint1 { get; set; }

        public Point XAxisPoint2 { get; set; }

        public Point YAxisPoint1 { get; set; }

        public Point YAxisPoint2 { get; set; }

        protected override void RenderCore(DrawingContext dc)
        {
            dc.DrawLine(Pens.Black, this.XAxisPoint1, this.XAxisPoint2);
            dc.DrawLine(Pens.Black, this.YAxisPoint1, this.YAxisPoint2);
        }
    }
}
