using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodioCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodioCore.Utils.ItemFields
{
    public class PhoneItemField : ItemField
    {
        /// <summary>
        /// Get or Set PhoneNumbers
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
}
