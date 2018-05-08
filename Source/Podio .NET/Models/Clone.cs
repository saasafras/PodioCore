using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioCore.Models
{
    public class Clone
    {
        [JsonProperty("files")]
        public List<FileAttachment> Files { get; set; }

        [JsonProperty("values")]
        public Dictionary<string, object> Values { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}