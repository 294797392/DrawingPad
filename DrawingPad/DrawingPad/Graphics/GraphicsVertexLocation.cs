using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Graphics
{
    /// <summary>
    /// 描述一个图形中的顶点的位置
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

        /// <summary>
        /// 左上角顶点
        /// </summary>
        TopLeft,

        /// <summary>
        /// 右上角顶点
        /// </summary>
        TopRight,

        /// <summary>
        /// 左下角顶点
        /// </summary>
        BottomLeft,

        /// <summary>
        /// 右下角顶点
        /// </summary>
        BottomRight
    }
}