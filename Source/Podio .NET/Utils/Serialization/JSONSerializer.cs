using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PodioCore.Utils
{
    internal class JSONSerializer
    {
        public static string Serilaize(object entity)
        {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;

            return JsonConvert.SerializeObject(entity, settings);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}