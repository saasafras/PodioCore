﻿using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PodioCore.Models;

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