using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad
{
    /// <summary>
    /// 表示连接点的位置
    /// 
    /// 所有图形的连接点的共同的特征：每个图形都有4个顶点，上下左右分别一个，通过这四个顶点规定连接线该如何画
    /// </summary>
    public enum ConnectionLocations
    {
        /// <summary>
        /// 连接点在上面
        /// </summary>
        Top,

        /// <summary>
        /// 连接点在左边
        /// </summary>
        Left,

        /// <summary>
        /// 连接点在右边
        /// </summary>
        Right,

        /// <summary>
        /// 连接点在下面
        /// </summary>
        Bottom,
    }
}
