﻿using System.Collections.Generic;

namespace PodioCore.Models
{
    public class PodioResponse
    {
        public dynamic Body { get; set; }
        public int Status { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string RequestUri { get; set; }
    }
}