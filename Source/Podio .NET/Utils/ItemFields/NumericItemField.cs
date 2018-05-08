using System.Linq;
using PodioCore.Models;

namespace PodioCore.Utils.ItemFields
{
    public class NumericItemField : ItemField
    {
        public double? Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return (double) this.Values.First()["value"];
                }
                else
                {
                    return null;
                }
            }

            set
            {
                EnsureValuesInitialized(true);
                this.Values.First()["value"] = value;
            }
        }
    }
}