using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodioCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioCore.Utils.ItemFields
{
    public class EmailItemField : ItemField
    {
        /// <summary>
        /// Get or Set Emails
        /// </summary>
        public IEnumerable<EmailPhoneFieldResult> Value
        {
            get
            {
                if (this.Values != null && this.Values.Any())
                    return this.Values.ToObject<List<EmailPhoneFieldResult>>();
                else
                    return new List<EmailPhoneFieldResult>();
            }

            set
            {
                
                EnsureValuesInitialized(true);
                this.Values = JArray.FromObject(value);
            }
        }
    }

    public class EmailPhoneFieldResult
    {
        /// <summary>
        /// Possible types for Email: other|home|work
        /// <para>Possible types for Phone: mobile|work|home|main|work_fax|private|fax|other </para>
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
