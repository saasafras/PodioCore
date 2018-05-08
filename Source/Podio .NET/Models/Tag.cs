using Newtonsoft.Json;

namespace PodioCore.Models
{
    public class Tag
    {
        [JsonProperty("count")]
        public int? Count { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}