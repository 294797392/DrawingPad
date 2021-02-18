using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciencePad
{
    /// <summary>
    /// 表示一个函数
    /// </summary>
    public interface IFunction
    {
        /// <summary>
        /// 计算函数值
        /// </summary>
        /// <param name="inputX">自变量X</param>
        /// <returns></returns>
        double Calculate(double inputX);
    }
}
