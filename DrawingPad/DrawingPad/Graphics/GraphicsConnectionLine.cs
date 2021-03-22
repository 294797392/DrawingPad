using DrawingPad.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    public class GraphicsConnectionLine : GraphicsBase
    {
        public override GraphicsType Type { get { return GraphicsType.ConnectionLine; } }

        /// <summary>
        /// 连接点
        /// </summary>
        public Point ConnectionPoint { get; set; }

        /// <summary>
        /// 起始点的位置
        /// </summary>
        public GraphicsVertexPosition StartPointPosition { get; set; }

        public DrawableVisual StartVisual { get; set; }

        public DrawableVisual TargetVisual { get; set; }

        public override void UpdatePosition(double offsetX, double offsetY)
        {
            throw new NotImplementedException();
        }

        public override void Resize(GraphicsVertexPosition vertex, Point oldPos, Point newPos)
        {
            throw new NotImplementedException();
        }
    }
}
