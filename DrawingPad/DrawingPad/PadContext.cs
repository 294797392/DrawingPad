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

        public const int CircleTrackerRadius = 4;

        public const int RectangleTrackerSize = 8;

        #endregion

        #region 画刷定义

        public static readonly Brush TrackerBackground = Brushes.White;

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
