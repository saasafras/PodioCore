using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioCore.Models;

namespace PodioCore.Utils.ItemFields
{
    public class AppItemField : ItemField
    {
        public IEnumerable<Item> Items
        {
            get { return this.ValuesAs<Item>(); }
        }

        public IEnumerable<int> ItemIds
        {
            set
            {
                EnsureValuesInitialized();
                foreach (var itemId in value)
                {
                    var jobject = new JObject();
                    jobject["value"] = itemId;
                    this.Values.Add(jobject);
                }
            }
        }

        public int ItemId
        {
            set
            {
                EnsureValuesInitialized();

                var jobject = new JObject();
                jobject["value"] = value;
                this.Values.Add(jobject);
            }
        }
    }
}