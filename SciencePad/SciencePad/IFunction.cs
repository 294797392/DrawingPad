using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SciencePad
{
    /// <summary>
    /// 表示一个函数
    /// </summary>
    public abstract class IFunction
    {
        /// <summary>
        /// 
        /// </summary>
        public Pen LinePen { get; set; }

        /// <summary>
        /// 计算函数值
        /// </summary>
        /// <param name="inputX">自变量X</param>
        /// <returns></returns>
        public abstract double Calculate(double inputX);
    }
}
