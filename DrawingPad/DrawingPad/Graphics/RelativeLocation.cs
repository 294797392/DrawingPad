using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Graphics
{
    /// <summary>
    /// 相对于图形的位置
    /// </summary>
    public enum RelativeLocation
    {
        /// <summary>
        /// 在图形左边
        /// </summary>
        Left,

        /// <summary>
        /// 在图形右边
        /// </summary>
        Right,

        /// <summary>
        /// 在图形上面
        /// </summary>
        Top,

        /// <summary>
        /// 在图形下面
        /// </summary>
        Bottom,

        /// <summary>
        /// 在左下方
        /// </summary>
        BottomLeft,

        /// <summary>
        /// 在右下方
        /// </summary>
        BottomRight,

        /// <summary>
        /// 在右上方
        /// </summary>
        TopRight,

        /// <summary>
        /// 在左上方
        /// </summary>
        TopLeft,

        /// <summary>
        /// 在图形内部
        /// </summary>
        Inner,
    }
}
