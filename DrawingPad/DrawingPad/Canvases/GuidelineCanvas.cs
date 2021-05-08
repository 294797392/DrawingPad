using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrawingPad.Canvases
{
    public enum GuidelineSize
    {
        /// <summary>
        /// 小
        /// </summary>
        Small,

        /// <summary>
        /// 中等
        /// </summary>
        Middle,

        /// <summary>
        /// 大
        /// </summary>
        Large,

        /// <summary>
        /// 更大
        /// </summary>
        ExtraLarge
    }

    /// <summary>
    /// 导航线图层
    /// 画一个导航线背景
    /// </summary>
    public class GuidelineCanvas : UserControl
    {
        #region 常量

        private static readonly Pen DefaultGridLinePen = new Pen(new SolidColorBrush(Color.FromRgb(238, 238, 238)), 1);
        private static readonly Pen DefaultGridLinePen2 = new Pen(new SolidColorBrush(Color.FromRgb(208, 208, 208)), 1);

        private static readonly Brush DefaultBackground = new SolidColorBrush(Color.FromRgb(242, 242, 242));

        private const int DefaultMargin = 0;

        #endregion

        #region 实例变量

        private Dictionary<GuidelineSize, int> GuidelineSizeMap = new Dictionary<GuidelineSize, int>()
        {
            { GuidelineSize.Small, 10 }, { GuidelineSize.Middle, 15 },
            { GuidelineSize.Large, 20 }, { GuidelineSize.ExtraLarge, 25 }
        };

        #endregion

        #region 属性

        public int GuidelineMargin
        {
            get { return (int)GetValue(GuidelineMarginProperty); }
            set { SetValue(GuidelineMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GuidelineMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GuidelineMarginProperty =
            DependencyProperty.Register("GuidelineMargin", typeof(int), typeof(GuidelineCanvas), new PropertyMetadata(DefaultMargin));



        public GuidelineSize GuidelineSize
        {
            get { return (GuidelineSize)GetValue(GuidelineSizeProperty); }
            set { SetValue(GuidelineSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GuidelineSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GuidelineSizeProperty =
            DependencyProperty.Register("GuidelineSize", typeof(GuidelineSize), typeof(GuidelineCanvas), new PropertyMetadata(GuidelineSize.Middle));

        #endregion

        #region 构造方法

        public GuidelineCanvas()
        {
        }

        #endregion

        #region 重写方法

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            // 画背景颜色
            Rect rect = new Rect(this.RenderSize);
            //dc.DrawRectangle(DefaultBackground, null, rect);

            // 画导航线的白色背景
            //rect.Inflate(-this.GuidelineMargin, -this.GuidelineMargin);
            dc.DrawRectangle(Brushes.White, null, rect);

            // 画导航线
            this.DrawGuideline(dc, rect.Width, rect.Height);
        }

        #endregion

        #region 实例方法

        private void DrawGuideline(DrawingContext dc, double width, double height, int size, Pen pen)
        {
            int unitx = (int)Math.Ceiling(width / size); // 一共要画多少个单位

            for (int index = 0; index < unitx; index++)
            {
                int offset = index * size;
                offset += this.GuidelineMargin;

                if (index == 0)
                {
                    continue;
                }

                Point startYPoint = new Point(offset, this.GuidelineMargin);
                Point endYPoint = new Point(offset, height + this.GuidelineMargin);
                dc.DrawLine(pen, startYPoint, endYPoint);
            }

            int unity = (int)Math.Ceiling(height / size);

            for (int index = 0; index < unity; index++)
            {
                int offset = index * size;
                offset += this.GuidelineMargin;

                if (index == 0)
                {
                    continue;
                }

                Point startXPoint = new Point(this.GuidelineMargin, offset);
                Point endXPoint = new Point(width + this.GuidelineMargin, offset);
                dc.DrawLine(pen, startXPoint, endXPoint);
            }
        }

        /// <summary>
        /// 画网格线
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="upp"></param>
        private void DrawGuideline(DrawingContext dc, double width, double height)
        {
            int size = this.GuidelineSizeMap[this.GuidelineSize];

            this.DrawGuideline(dc, width, height, size, DefaultGridLinePen);            // 小的网格线

            this.DrawGuideline(dc, width, height, size * 4, DefaultGridLinePen2);      // 大的网格线
        }

        #endregion
    }
}
