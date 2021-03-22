using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawingPad
{
    public class PadContext
    {
        #region 常量定义

        private const int DefaultWidth = 1;
        private const int LineWidth = 2;

        public const int CircleTrackerRadius = 4;

        public const int RectangleTrackerSize = 8;

        /// <summary>
        /// 连接线到图形的最小边距
        /// </summary>
        public const int MinimalMargin = 20;

        /// <summary>
        /// 缩放图形的时候，图形最小的大小
        /// </summary>
        public const int MinimalVisualSize = 30;

        #endregion

        #region 画刷定义

        public static readonly Brush TrackerBackground = Brushes.White;

        public static readonly Brush LineBrush = Brushes.Black;
        public static readonly Pen LinePen = new Pen(Brushes.Black, LineWidth);

        #endregion

        #region 画笔定义

        public static readonly Pen TrackerPen = new Pen(Brushes.Black, DefaultWidth);

        #endregion

        #region 常量定义

        /// <summary>
        /// 每个单位长度所对应的像素数量
        /// upp
        /// </summary>
        //public const int UnitPerPixel = 50;

        #endregion

        private static PadContext context = new PadContext();

        public static PadContext Context { get { return context; } }
    }
}
