using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Graphics
{
    /// <summary>
    /// 缩放点的位置
    /// 
    /// 所有图形的缩放点的共同的特征：每个图形都有4个顶点，左上，左下，右上，右下分别一个，通过这四个顶点来缩放图形
    /// </summary>
    public enum ResizeLocations
    {
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
