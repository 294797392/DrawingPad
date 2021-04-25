using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Graphics
{
    /// <summary>
    /// 描述一个图形中的顶点的位置
    /// 
    /// 所有图形的共同的特征：每个图形都有4个顶点，上下左右分别一个，通过这四个顶点规定连接线该如何画
    /// </summary>
    public enum GraphicsVertexLocation
    {
        /// <summary>
        /// 上边中间点顶点
        /// </summary>
        TopCenter,

        /// <summary>
        /// 左边中间点顶点
        /// </summary>
        LeftCenter,

        /// <summary>
        /// 右边中间点顶点
        /// </summary>
        RightCenter,

        /// <summary>
        /// 下边中间点顶点
        /// </summary>
        BottomCenter,

    }
}