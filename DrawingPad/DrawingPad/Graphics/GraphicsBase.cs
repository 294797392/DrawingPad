using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    public abstract class GraphicsBase
    {
        public abstract GraphicsType Type { get; }

        /// <summary>
        /// 角度
        /// </summary>
        public double Angle { get; set; }

        public abstract void UpdatePosition(double offsetX, double offsetY);

        /// <summary>
        /// 对图形进行调整大小的操作
        /// </summary>
        /// <param name="vertexPos">被调整大小的顶点位置</param>
        /// <param name="vertexPoint">顶点的新位置</param>
        public abstract void Resize(GraphicsVertexPosition vertexPos, Point oldPos, Point newPos);

        public abstract Point GetResizeHandle(int index);

        public abstract Point GetConnectionHandle(int index);

        public abstract Point GetRotationHandle();
    }
}
