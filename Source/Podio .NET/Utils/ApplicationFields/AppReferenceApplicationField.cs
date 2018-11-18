using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioCore.Models;
using Newtonsoft.Json;
namespace PodioCore.Utils.ApplicationFields
{

    public class AppReferenceApplicationField : ApplicationField
    {
        private IEnumerable<int> _referenceableTypes;

        /// <summary>
        ///     List of ids of the apps that can be referenced.
        /// </summary>
        public IEnumerable<int> ReferenceableTypes
        {
            get
            {
                if (_referenceableTypes == null)
                {
                    _referenceableTypes = this.GetSettingsAs<int>("referenceable_types");
                }
                return _referenceableTypes;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["referenceable_types"] = value != null ? JToken.FromObject(value) : null;
            }
        }

        private IEnumerable<ReferencedApplication> _referencedApps; 
        [JsonProperty(PropertyName = "apps",NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ReferencedApplication> ReferenceableApps
        {
            get
            {
                if(_referencedApps == null)
                {
                    _referencedApps = this.GetSettingsAs<ReferencedApplication>("apps");
                }
                return _referencedApps;
            }
        }

        /// <summary>
        ///     True if multiple references should be allowed, False otherwise
        /// </summary>
        public bool Multiple
        {
            get { return (bool)this.GetSetting("multiple"); }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["multiple"] = value;
            }
        }
    }
}