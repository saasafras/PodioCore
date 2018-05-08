using Newtonsoft.Json;

namespace PodioCore.Models
{
    public class Seat
    {
        [JsonProperty("employee")]
        public string Employee { get; set; }

        [JsonProperty("external")]
        public string External { get; set; }
    }
}