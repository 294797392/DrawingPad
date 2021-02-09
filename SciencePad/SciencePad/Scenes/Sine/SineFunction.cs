using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciencePad.Scenes.Sine
{
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
        /// 相位偏移
        /// </summary>
        public double Phase { get; set; }
    }
}
