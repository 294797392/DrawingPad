using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingPad.DataModels
{
    public class ToolboxGroup
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("items")]
        public List<ToolboxItem> Items { get; set; }

        public ToolboxGroup()
        {
            this.Items = new List<ToolboxItem>();
        }
    }
}
