using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SciencePad.Scenes.Sine
{
    /// <summary>
    /// 正弦曲线公式：y = Asin(ωt±θ)
    ///              A是振幅大小，表示正弦波形幅度的大小（Y轴的大小）
    ///              ω是角频率，表示正弦波震动的快慢（频率的高低，每秒钟的频次，频次多的就是高频，频次低的就是低频）
    ///              t是时间，表示X轴
    ///              θ是相位偏移，表示正弦波形的偏移量（X轴的偏移量）
    /// </summary>
    public class SineFunction : IFunction
    {
        /// <summary>
        /// 振幅
        /// </summary>
        public double Amplitude { get; set; }

        /// <summary>
        /// 正弦波的频率
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// 时间点，是一个自变量
        /// 函数值y根据时间的变化而变化
        /// </summary>
        //public double Time { get; set; }

        /// <summary>
        /// 相位
        /// </summary>
        public double Phase { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputX">自变量X是弧度</param>
        /// <returns></returns>
        public override double Calculate(double inputX)
        {
            inputX = inputX * this.Frequency;

            inputX += this.Phase;

            return Math.Sin(inputX) * this.Amplitude;
        }
    }
}
