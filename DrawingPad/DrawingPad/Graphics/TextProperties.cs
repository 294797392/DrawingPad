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
        /// 字体
        /// </summary>
        [JsonProperty("FontFamily")]
        public string FontFamily { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        [JsonProperty("FontSize")]
        public int FontSize { get; set; }

        /// <summary>
        /// 是否是加粗
        /// </summary>
        [JsonProperty("Bold")]
        public bool Bold { get; set; }

        /// <summary>
        /// 是否倾斜
        /// </summary>
        [JsonProperty("Italic")]
        public bool Italic { get; set; }

        /// <summary>
        /// 是否有下划线
        /// </summary>
        [JsonProperty("Underline")]
        public bool Underline { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [JsonProperty("Color")]
        public string Color { get; set; }

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
