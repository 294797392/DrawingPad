using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SciencePad.Scenes
{
    /// <summary>
    /// 坐标系原点位置
    /// </summary>
    public enum CoordinateOriginalPositions
    {
        /// <summary>
        /// 界面中间位置
        /// </summary>
        Center,

        /// <summary>
        /// 左下角
        /// </summary>
        LeftBottom,
    }

    /// <summary>
    /// 坐标系场景
    /// 画一个坐标系
    /// </summary>
    public class CoordinateScene : VisualScene
    {
        #region 常量

        private const int DefaultThinkess = 1;

        private static readonly Brush BorderBrush = new SolidColorBrush(Color.FromRgb(84, 84, 84));
        private static readonly Brush DefaultFontBrush = new SolidColorBrush(Colors.Black);

        private static readonly Pen AxisPen = new Pen(BorderBrush, DefaultThinkess);
        private static readonly Pen BorderPen = new Pen(BorderBrush, DefaultThinkess);
        private static readonly Pen DefaultGridLinePen = new Pen(new SolidColorBrush(Color.FromRgb(217, 217, 217)), DefaultThinkess);

        private static readonly Typeface DefaultTypeFace = new Typeface(SystemFonts.MessageFontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

        private const CoordinateOriginalPositions DefaultOriginalPosition = CoordinateOriginalPositions.LeftBottom;

        private const int DefaultFontSize = 12;

        private const int DefaultUnitPerPixel = 30;

        #endregion

        #region 实例变量

        private Typeface fontFace;
        private int fontSize;
        private Brush fontBrush;
        private double pixelPerDip;
        private Pen gridLinePen;

        #endregion

        #region 属性

        /// <summary>
        /// 坐标轴原点位置
        /// </summary>
        public CoordinateOriginalPositions OriginalPosition { get; set; }

        /// <summary>
        /// 坐标轴原点
        /// </summary>
        public Point OriginalPoint { get; private set; }

        /// <summary>
        /// 是否画网格线
        /// </summary>
        public bool IsDrawGridLine { get; set; }

        /// <summary>
        /// 是否画坐标轴
        /// </summary>
        public bool IsDrawAxis { get; set; }

        /// <summary>
        /// 是否画数值
        /// </summary>
        public bool IsDrawCoordinate { get; set; }

        /// <summary>
        /// 一个单位长度是多少像素
        /// </summary>
        public int UnitPerPixel { get; set; }

        #endregion

        #region 构造方法

        public CoordinateScene()
        {
            this.UnitPerPixel = DefaultUnitPerPixel;
            this.fontFace = DefaultTypeFace;
            this.fontSize = DefaultFontSize;
            this.fontBrush = DefaultFontBrush;
            this.gridLinePen = DefaultGridLinePen;

            //this.pixelPerDip = VisualTreeHelper.GetDpi(this).PixelsPerDip;

            this.IsDrawCoordinate = true;
            this.IsDrawAxis = true;
            this.IsDrawCoordinate = true;
            this.OriginalPosition = DefaultOriginalPosition;
        }

        #endregion

        #region 重写方法

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (this.IsDrawCoordinate)
            {
                this.DrawCoordinate(dc, this.UnitPerPixel, this.OriginalPosition);
            }

            if (this.IsDrawGridLine)
            {
                this.DrawGridLine(dc, this.UnitPerPixel);
            }

            if (this.IsDrawAxis)
            {
                this.DrawAxis(dc, this.OriginalPosition);
            }
        }

        #endregion

        #region 实例方法

        private void GetAxisXYPoints(CoordinateOriginalPositions original, out Point startYPoint, out Point endYPoint, out Point startXPoint, out Point endXPoint, out Point originalPos)
        {
            switch (original)
            {
                case CoordinateOriginalPositions.Center:
                    {
                        // 画Y轴
                        startYPoint = new Point(this.Width / 2, 0);
                        endYPoint = new Point(this.Width / 2, this.Height);
                        startXPoint = new Point(0, this.Height / 2);
                        endXPoint = new Point(this.Width, this.Height / 2);
                        originalPos = new Point(this.Width / 2, this.Height / 2);
                        break;
                    }

                case CoordinateOriginalPositions.LeftBottom:
                    {
                        startYPoint = new Point(0, 0);
                        endYPoint = new Point(0, this.Height);
                        startXPoint = new Point(0, this.Height);
                        endXPoint = new Point(this.Width, this.Height);
                        originalPos = new Point(0, this.Height);
                        break;
                    }

                default:
                    throw new NotImplementedException();
            }
        }

        private FormattedText CreateYAxisCoordinate(int valueY)
        {
            return new FormattedText(valueY.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.fontFace, this.fontSize, this.fontBrush)
            {
                TextAlignment = TextAlignment.Right
            };
        }

        private FormattedText CreateXAxisCoordinate(int valueX)
        {
            return new FormattedText(valueX.ToString(), System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, this.fontFace, this.fontSize, this.fontBrush)
            {
                TextAlignment = TextAlignment.Center
            };
        }

        /// <summary>
        /// 画XY轴
        /// </summary>
        /// <param name="dc"></param>
        private void DrawAxis(DrawingContext dc, CoordinateOriginalPositions original)
        {
            Point startYPoint, endYPoint, startXPoint, endXPoint, originalPos;
            this.GetAxisXYPoints(original, out startYPoint, out endYPoint, out startXPoint, out endXPoint, out originalPos);

            // 画Y轴
            dc.DrawLine(AxisPen, startYPoint, endYPoint);

            // 画X轴
            dc.DrawLine(AxisPen, startXPoint, endXPoint);

            this.OriginalPoint = originalPos;

            // 画边框
            Rect borderRect = new Rect()
            {
                Width = this.Width,
                Height = this.Height,
                X = 0,
                Y = 0,
            };
            dc.DrawRectangle(Brushes.Transparent, BorderPen, borderRect);
        }

        /// <summary>
        /// 画坐标轴上的数值
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="upp">unit per pixel，每个单位是多少像素</param>
        private void DrawCoordinate(DrawingContext dc, int upp, CoordinateOriginalPositions originalPos)
        {
            int unit = (int)Math.Ceiling(this.Width / upp); // 一共要画多少个单位

            switch (originalPos)
            {
                #region 坐标轴原点在界面中间

                case CoordinateOriginalPositions.Center:
                    {
                        break;
                    }

                    #endregion

                #region 坐标轴原点在界面左下角

                case CoordinateOriginalPositions.LeftBottom:
                    {
                        break;
                    }

                    #endregion
            }

            int valueY = unit / 2; // Y轴的起始点坐标
            int valueX = -valueY; // X轴的起始点坐标

            for (int index = 0; index < unit; index++)
            {
                double offset = upp * index;   // X和Y轴坐标的偏移量

                Point valueYPoint = new Point(-this.fontSize, offset);
                dc.DrawText(this.CreateYAxisCoordinate(valueY), valueYPoint);

                Point valueXPoint = new Point(offset, this.Height + this.fontSize);
                dc.DrawText(this.CreateXAxisCoordinate(valueX), valueXPoint);

                valueY--;
                valueX++;
            }
        }

        /// <summary>
        /// 画网格线
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="upp"></param>
        private void DrawGridLine(DrawingContext dc, int upp)
        {
            int unit = (int)Math.Ceiling(this.Width / upp); // 一共要画多少个单位

            for (int index = 0; index < unit; index++)
            {
                int offset = index * upp;

                Point startXPoint = new Point(0, offset);
                Point endXPoint = new Point(this.Width, offset);
                dc.DrawLine(this.gridLinePen, startXPoint, endXPoint);

                Point startYPoint = new Point(offset, 0);
                Point endYPoint = new Point(offset, this.Height);
                dc.DrawLine(this.gridLinePen, startYPoint, endYPoint);
            }
        }

        #endregion
    }
}
