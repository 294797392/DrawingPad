using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciencePad
{
    public class PadContext
    {
        #region 常量定义

        /// <summary>
        /// 每个单位长度所对应的像素数量
        /// upp
        /// </summary>
        public const int UnitPerPixel = 50;

        #endregion

        private static PadContext context = new PadContext();

        public static PadContext Context { get { return context; } }

    }
}
