using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Graphics
{
    public abstract class GraphicsBase
    {
        public abstract GraphicsType Type { get; }

        /// <summary>
        /// 角度
        /// </summary>
        public double Angle { get; set; }
    }
}
