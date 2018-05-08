using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PodioCore.Models;
using PodioCore.Utils.ItemFields;

namespace PodioCore.Utils.ApplicationFields
{
    public class QuestionApplicationField : ApplicationField
    {
        [JsonIgnore] private IEnumerable<CategoryItemField.Option> _options;

        /// <summary>
        ///     The list of options for the question
        /// </summary>
        public IEnumerable<CategoryItemField.Option> Options
        {
            get
            {
                if (_options == null)
                {
                    _options = this.GetSettingsAs<CategoryItemField.Option>("options");
                }
                return _options;
            }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["options"] = value != null ? JToken.FromObject(value) : null;
            }
        }

        /// <summary>
        ///     True if multiple options should be allowed, False otherwise
        /// </summary>
        public bool Multiple
        {
            get { return (bool) this.GetSetting("multiple"); }
            set
            {
                InitializeFieldSettings();
                this.InternalConfig.Settings["multiple"] = value;
            }
        }
    }
}