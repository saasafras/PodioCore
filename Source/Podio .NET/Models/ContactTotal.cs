﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioCore.Models
{
    public class ContactTotal
    {
        [JsonProperty("count")]
        public int TotalCount { get; set; }

        [JsonProperty("orgs")]
        public List<OrganizationContactTotal> Orgs { get; set; }
    }
}