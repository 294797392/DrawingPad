using DrawingPad.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.DataModels
{
    public class ToolboxItem
    {
        /// <summary>
        /// 图形编号
        /// </summary>
        [JsonProperty("id")]
        public string ID { get; set; }

        /// <summary>
        /// 图形名字
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 图形图标URI
        /// </summary>
        [JsonProperty("Icon")]
        public string Icon { get; set; }

        /// <summary>
        /// 图形类型
        /// </summary>
        [JsonProperty("type")]
        public GraphicsType Type { get; set; }
    }
}
