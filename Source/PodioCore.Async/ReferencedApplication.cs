using Newtonsoft.Json;
namespace PodioCore.Utils.ApplicationFields
{
    public class ReferencedApplication
    {
        [JsonProperty(PropertyName = "space_id")]
        public int? SpaceId { get; set; }
        [JsonProperty(PropertyName = "app_id")]
        public int? AppId { get; set; }
    }
}