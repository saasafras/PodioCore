using System.Linq;
using PodioCore.Models;

namespace PodioCore.Utils.ItemFields
{
    public class StateItemField : ItemField
    {
        public string Value
        {
            get
            {
                if (this.HasValue("value"))
                {
                    return (string) this.Values.First()["value"];
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