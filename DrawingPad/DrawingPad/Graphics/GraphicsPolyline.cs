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
        #region 公开属性
        
        public override GraphicsType Type { get { return GraphicsType.Polyline; } }

        /// <summary>
        /// 折线的点列表
        /// </summary>
        public List<Point> PointList { get; set; }

        /// <summary>
        /// 该折线的终点坐标
        /// 箭头就要画在这个方向
        /// </summary>
        public Point Termination { get; set; }

        /// <summary>
        /// 被连接的第一个图形的ID
        /// 如果为空就说明没有
        /// 1 -> 2
        /// </summary>
        public string AssociatedGraphics1 { get; set; }

        /// <summary>
        /// 第一个图形的点的Location
        /// </summary>
        public int Graphics1Handle { get; set; }

        /// <summary>
        /// 被连接的第二个图形的ID
        /// 1 -> 2
        /// </summary>
        public string AssociatedGraphics2 { get; set; }

        /// <summary>
        /// 第二个图形的点的Location
        /// </summary>
        public int Graphics2Handle { get; set; }

        #endregion

        #region 构造方法

        public GraphicsPolyline()
        {
            this.PointList = new List<Point>();
        }

        #endregion

        #region GraphicsBase

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

        public override Point GetConnectionPoint(int index)
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

        public override ConnectionLocations GetConnectorLocation(int handleIndex)
        {
            throw new NotImplementedException();
        }

        public override ResizeLocations GetResizeLocation(Point handlePoint)
        {
            throw new NotImplementedException();
        }

        public override Rect GetConnectorBounds(int index)
        {
            throw new NotImplementedException();
        }

        public override Rect GetResizeHandleBounds(int index)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
