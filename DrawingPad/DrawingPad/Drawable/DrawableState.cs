using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Drawable
{
    /// <summary>
    /// 标识当前画板的状态
    /// </summary>
    public enum DrawableState
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Idle,

        /// <summary>
        /// 在做平移操作
        /// </summary>
        Translate,

        /// <summary>
        /// 在画连接线
        /// </summary>
        DrawConnectionLine,

        /// <summary>
        /// 在做调整大小的操作
        /// </summary>
        Resizing,

        /// <summary>
        /// 当前是输入状态（双击图形后触发）
        /// </summary>
        InputState,
    }
}
