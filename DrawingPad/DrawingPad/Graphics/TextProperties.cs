using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DrawingPad.Graphics
{
    /// <summary>
    /// 保存文本属性
    /// </summary>
    public class TextProperties
    {
        /// <summary>
        /// 图形上显示的文本
        /// </summary>
        [JsonProperty("Text")]
        public string Text { get; set; }

        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        [JsonProperty("TextAlignment")]
        public VerticalAlignment VerticalAlignment { get; set; }

        /// <summary>
        /// 水平对齐方式
        /// </summary>
        [JsonProperty("HorizontalAlignment")]
        public HorizontalAlignment HorizontalAlignment { get; set; }

        public TextProperties()
        {
            this.VerticalAlignment = VerticalAlignment.Center;
            this.HorizontalAlignment = HorizontalAlignment.Center;
        }
    }
}
