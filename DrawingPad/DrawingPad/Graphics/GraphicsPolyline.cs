using DrawingPad.Drawable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    public class GraphicsPolyline : GraphicsBase
    {
        public override GraphicsType Type { get { return GraphicsType.Polyline; } }

        /// <summary>
        /// 连接点
        /// </summary>
        public Point ConnectionPoint { get; set; }

        /// <summary>
        /// 起始点的位置
        /// </summary>
        public ConnectionLocations StartConnectionLocation { get; set; }

        public DrawableVisual StartVisual { get; set; }

        public DrawableVisual TargetVisual { get; set; }

        public override void Translate(double offsetX, double offsetY)
        {
            throw new NotImplementedException();
        }

        public override void Resize(ResizeLocations location, Point oldPos, Point newPos)
        {
            throw new NotImplementedException();
        }

        public override Point GetResizeHandle(int index)
        {
            throw new NotImplementedException();
        }

        public override Point GetConnectionHandle(int index)
        {
            throw new NotImplementedException();
        }

        public override Point GetRotationHandle()
        {
            throw new NotImplementedException();
        }

        public override Rect GetBounds()
        {
            throw new NotImplementedException();
        }

        public override ConnectionLocations GetConnectionLocation(Point handlePoint)
        {
            throw new NotImplementedException();
        }

        public override ResizeLocations GetResizeLocation(Point handlePoint)
        {
            throw new NotImplementedException();
        }

        public override Rect GetConnectionHandleBounds(int index)
        {
            throw new NotImplementedException();
        }

        public override Rect GetResizeHandleBounds(int index)
        {
            throw new NotImplementedException();
        }
    }
}
