﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioCore.Models
{
    public class AppMarketCategory
    {
        [JsonProperty("functional")]
        public JArray Functional { get; set; }

        [JsonProperty("vertical")]
        public JArray Vertical { get; set; }
    }
}