using DrawingPad.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFToolkit.MVVM;

namespace DrawingPad.ViewModels
{
    public class GraphicsVM :ItemViewModel
    {
        /// <summary>
        /// 图形类型
        /// </summary>
        public GraphicsType Type { get; set; }
    }
}
