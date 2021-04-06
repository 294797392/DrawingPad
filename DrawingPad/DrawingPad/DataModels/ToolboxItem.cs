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
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("graphics")]
        public string Graphics { get; set; }

        [JsonProperty("drawable")]
        public string Drawable { get; set; }
    }
}
