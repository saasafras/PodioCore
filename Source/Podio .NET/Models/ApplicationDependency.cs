using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodioCore.Utils;

namespace PodioCore.Models
{
    public class ApplicationDependency
    {
        [JsonProperty("apps")]
        public List<Application> Apps { get; set; }

        [JsonProperty("dependencies")]
        private JToken DependencyObject { get; set; }
    }
}