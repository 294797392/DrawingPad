using SciencePad.Visuals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SciencePad
{
    public class DrawingCanavs : Canvas
    {
        #region 常量

        private const int UnitPerPixel = 50;        // 一个单位等于20个像素

        private static readonly Pen AxisPen = new Pen(Brushes.Silver, 2);
        private static readonly Pen BorderPen = new Pen(Brushes.Silver, 2);

        #endregion

        #region 实例变量

        /// <summary>
        /// 当前画板上的所有的图形集合
        /// </summary>
        private List<VisualCircle> geometryList;

        #endregion

        #region 属性

        /// <summary>
        /// 坐标轴原点
        /// </summary>
        public Point OriginalPoint { get; private set; }

        #endregion

        #region 构造方法

        public DrawingCanavs()
        {
            this.UseLayoutRounding = true;
            this.Background = Brushes.White;

            this.geometryList = new List<ScienceGeometry>();
        }

        #endregion

        #region 重写函数

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            this.DrawAxis(dc);
        }

        #endregion

        #region 公开接口

        /// <summary>
        /// 画一个单位圆
        /// 单位圆指的是平面直角坐标系上，圆心为原点，半径为单位长度的圆
        /// </summary>
        public void DrawCircle(int unitLength)
        {
            double pixelRadius = unitLength * UnitPerPixel;
            VisualCircle circle = new VisualCircle(this.OriginalPoint.X, this.OriginalPoint.Y, pixelRadius, Brushes.Black);
            this.geometryList.Add(circle);
            this.InvalidateVisual();
        }

        public void DrawTriangle()
        {
        }

        #endregion

        #region 实例方法

        private void DrawAxis(DrawingContext dc)
        {
            // 画Y轴
            Point startYPoint = new Point(this.Width / 2, 0);
            Point endYPoint = new Point(this.Width / 2, this.Height);
            dc.DrawLine(AxisPen, startYPoint, endYPoint);

            // 画X轴
            Point startXPoint = new Point(0, this.Height / 2);
            Point endXPoint = new Point(this.Width, this.Height / 2);
            dc.DrawLine(AxisPen, startXPoint, endXPoint);

            this.OriginalPoint = new Point(this.Width / 2, this.Height / 2);

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

        #endregion
    }
}



