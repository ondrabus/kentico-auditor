
using Newtonsoft.Json;

namespace Auditor.Core.Models
{
    public class DataField
    {
        [JsonProperty(PropertyName = "n")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "o_v")]
        public string OldValue { get; set; }
        [JsonProperty(PropertyName = "v")]
        public string Value { get; set; }
    }
}