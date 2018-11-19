using System;
using PodioCore.Models;
using PodioCore.Utils.ApplicationFields;

namespace PodioCore.Async
{
    public static class FieldConversionExtensions
    {
        public static AppReferenceApplicationField GetAppReferenceAppField(this Application app, int fieldId)
        {
            var field = app.Field<AppReferenceApplicationField>(fieldId);
            return field;
        }
    }
}
