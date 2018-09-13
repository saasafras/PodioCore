using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PodioCore.Models
{
    public class Config
    {
        [JsonProperty("layout", NullValueHandling = NullValueHandling.Ignore)]
        public string Layout { get; set; }
        [JsonProperty("target_value", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetValue { get; set; }
        [JsonProperty("app_id", NullValueHandling = NullValueHandling.Ignore)]
        public int AppId { get; set; }
        [JsonProperty("app_link", NullValueHandling = NullValueHandling.Ignore)]
        public string AppLink { get; set; }
        [JsonProperty("calculation", NullValueHandling = NullValueHandling.Ignore)]
        public Calculation Calculation { get; set; }
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty("file_id", NullValueHandling = NullValueHandling.Ignore)]
        public int FileId { get; set; }
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int Limit { get; set; }
    }
    public class Grouping
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        [JsonProperty("sub_value", NullValueHandling = NullValueHandling.Ignore)]
        public string SubValue { get; set; }
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
    public class Calculation
    {
        [JsonProperty("sorting", NullValueHandling = NullValueHandling.Ignore)]
        public string Sorting { get; set; }
        [JsonProperty("aggregation", NullValueHandling = NullValueHandling.Ignore)]
        public string Aggregation { get; set; }
        [JsonProperty("filters", NullValueHandling = NullValueHandling.Ignore)]
        public Filters[] Filters { get; set; }
        [JsonProperty("formula", NullValueHandling = NullValueHandling.Ignore)]
        public Formula[] Formula { get; set; }
        [JsonProperty("grouping", NullValueHandling = NullValueHandling.Ignore)]
        public Grouping Grouping { get; set; }
        [JsonProperty("groupings", NullValueHandling = NullValueHandling.Ignore)]
        public Grouping[] Groupings { get; set; }
    }
    public class Filters
    {
        [JsonProperty("humanized_values", NullValueHandling = NullValueHandling.Ignore)]
        public JObject HumanizedValues { get; set; }
        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public Values Values { get; set; }
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }
    }
    public class Values
    {
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public string To { get; set; }
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public string From { get; set; }
    }
    public class Formula
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
    public class Widget
    {
        [JsonProperty("widget_id")]
        public int? WidgetId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("config")]
        public Config Config { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("data")]
        public JToken Data { get; set; }

        [JsonProperty("created_by")]
        public ByLine CreatedBy { get; set; }

        [JsonProperty("created_on")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("ref")]
        public Reference Ref { get; set; }

        [JsonProperty("allowed_refs")]
        public string[] AllowedRefs { get; set; }

        [JsonProperty("cols")]
        public int Cols { get; set; }

        [JsonProperty("rows")]
        public int Rows { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }
    }
}