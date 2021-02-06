using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SciencePad.Visuals
{
    public abstract class VisualGeometry : DrawingVisual
    {
        public void Render()
        {
            DrawingContext dc = this.RenderOpen();

            this.RenderCore(dc);

            dc.Close();
        }

        protected abstract void RenderCore(DrawingContext dc);
    }
}
