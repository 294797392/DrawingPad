using SciencePad.Scenes.Sine;
using SciencePad.Visuals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace SciencePad.Scenes
{
    /// <summary>
    /// 正弦三角函数公式：y = sin(x)
    /// 
    /// 正弦曲线公式：y = Asin(ωt±θ)
    ///              A是振幅大小，表示正弦波形幅度的大小（Y轴的大小）
    ///              ω是角频率，表示正弦波震动的快慢（频率的高低，每秒钟的频次，频次多的就是高频，频次低的就是低频）
    ///              t是时间，表示X轴
    ///              θ是相位偏移，表示正弦波形的偏移量（X轴的偏移量）
    /// 
    /// </summary>
    public class SineScene : FunctionScene
    {
        #region 常量

        #endregion

        #region 实例变量

        #endregion

        #region 属性

        #endregion

        #region 构造方法

        public SineScene()
        {
        }

        #endregion

        #region 实例方法

        private PathSegment CreateSineSegement(SineFunction sinFunc, out Point firstPoint)
        {
            PolyBezierSegment segement = new PolyBezierSegment();

            for (int angle = 0; angle < 360; angle++)
            {
                double x = Math.PI / 180 * angle;   // 自变量X的值

                double y = sinFunc.Calculate(x);

                y = this.OriginalPoint.Y + y * this.UnitPerPixel;

                //QuadraticBezierSegment qb = new QuadraticBezierSegment();
                //qb.Point1 = new Point(x, y);
                //qb.Point2 = new Point(x, y);
                //figure.Segments.Add(qb);
                segement.Points.Add(new Point(this.OriginalPoint.X + angle, y));
            }

            firstPoint = segement.Points[0];

            return segement;
        }

        #endregion

        #region 事件处理器

        public override Geometry CreateFunctionGeometry(IFunction function)
        {
            SineFunction sinFunc = function as SineFunction;

            Point startPoint;
            PathGeometry sineGeometry = new PathGeometry();
            PathFigure figure = new PathFigure();
            PathSegment sineSegement = this.CreateSineSegement(sinFunc, out startPoint);
            figure.Segments.Add(sineSegement);
            figure.StartPoint = startPoint;
            sineGeometry.Figures.Add(figure);
            return sineGeometry;
        }

        #endregion
    }
}

