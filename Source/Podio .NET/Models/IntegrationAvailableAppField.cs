using Newtonsoft.Json;

namespace PodioCore.Models
{
    public class IntegrationAvailableAppField
    {
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}