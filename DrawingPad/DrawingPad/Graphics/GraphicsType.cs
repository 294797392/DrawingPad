using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Graphics
{
    /// <summary>
    /// 表示图形类型
    /// </summary>
    public enum GraphicsType
    {
        /// <summary>
        /// 矩形
        /// </summary>
        Rectangle = 0,

        /// <summary>
        /// 椭圆
        /// </summary>
        Ellipse,

        /// <summary>
        /// 连接线
        /// </summary>
        Polyline
    }
}
