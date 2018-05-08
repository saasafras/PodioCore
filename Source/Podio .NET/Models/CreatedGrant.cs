using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioCore.Models
{
    public class CreatedGrant
    {
        [JsonProperty("invitable")]
        public List<User> Profiles { get; set; }
    }
}