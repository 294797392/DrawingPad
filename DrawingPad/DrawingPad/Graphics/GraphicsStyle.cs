using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.Graphics
{
    /// <summary>
    /// 表示图形的样式
    /// </summary>
    public class GraphicsStyle
    {
        /// <summary>
        /// 字体样式
        /// </summary>
        [JsonProperty("TextProperties")]
        public TextProperties TextProperties { get; set; }

        /// <summary>
        /// 填充画刷
        /// </summary>
        [JsonProperty("FillBrush")]
        public string FillBrush { get; set; }

        /// <summary>
        /// 线条画刷
        /// </summary>
        [JsonProperty("LineBrush")]
        public string LineBrush { get; set; }

        /// <summary>
        /// 线条宽度
        /// </summary>
        [JsonProperty("LineWidth")]
        public int LineWidth { get; set; }

        /// <summary>
        /// 线条样式
        /// </summary>
        [JsonProperty("LineStyle")]
        public int LineStyle { get; set; }
    }
}
