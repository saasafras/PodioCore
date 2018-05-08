using Newtonsoft.Json;

namespace PodioCore.Models
{
    public class ApplicationRevision
    {
        [JsonProperty("revision")]
        public string Revision { get; set; }
    }
}