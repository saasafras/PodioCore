using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using PodioCore.Models;
using PodioCore.Services;
namespace PodioCore.Applications
{
    public static class ApplicationServiceMethods
    {
        private static ApplicationService _service;
        private static bool initialized;
        private static void init(Podio client)
        {
            _service = new ApplicationService(client);
            initialized = true;
        }
        
        public static async Task<int> AddNewApp(this Podio client, Application newApp)
		{
			if (!initialized)
				init(client);
			
			return await _service.AddNewApp(newApp);
		}

		public static async Task<Application> GetApplication(this Podio client, int appId)
        {
            if (!initialized)
                init(client);

            var result = await _service.GetApp(appId, view: "micro");
            return result;
        }

		public static async Task<Application> GetFullApplication(this Podio client, int appId)
        {
            if (!initialized)
                init(client);

            var result = await _service.GetApp(appId, view: "full");
            return result;
        }

        public static async Task<int> CreateApplication(this Podio client, Application app)
        {
            if (!initialized)
                init(client);

            var result = await _service.AddNewApp(app);
            return result;
        }

        public static async Task UpdateApplication(this Podio client, Application app)
        {
            if (!initialized)
                init(client);

            await _service.UpdateApp(app, silent: false);
        }

        public static async Task<List<Application>> GetApplicationsInSpace(this Podio client, int spaceId)
        {
            if (!initialized)
                init(client);

            var result = await _service.GetAppsBySpace(spaceId);
            return result;
        }

        public static async Task<List<Application>> GetApplicationsInSpaceWithFields(this Podio client, int spaceId, Dictionary<string,string> attributes)
        {
            if (!initialized)
                init(client);
            
            var result = await _service.GetAppsBySpace(spaceId, false, attributes);
            return result;
        }

		public static async Task ToggleFieldVisibility(this Podio client, int appId, IEnumerable<int> fieldIds, bool hide, bool silent = true)
		{
			if (!initialized)
				init(client);

			var app = await _service.GetApp(appId, "full");
			foreach (var fieldId in fieldIds)
			{
				var field = app.Fields.First(f => f.FieldId == fieldId);
				field.Config.AlwaysHidden = hide;
			}
			await _service.UpdateApp(app, silent);
		}

        public static async Task<ApplicationField> GetAppField(this Podio client, int appId, int itemId)
		{
			if (!initialized)
				init(client);
			return await _service.GetAppField(appId, itemId);
		}
        public static async Task UpdateAppField(this Podio client, Application app)
		{
			if (!initialized)
				init(client);
			await _service.UpdateAnAppField(app);
		}
        public static async Task<int> AddNewAppField(this Podio client, int appId, ApplicationField appField)
		{
			if (!initialized)
				init(client);
			return await _service.AddNewAppField(appId, appField);
		}
        public static async Task DeleteAppField(this Podio client, int appId, int fieldId)
		{
			if (!initialized)
				init(client);
			await _service.DeleteAppField(appId, fieldId);
		}
    }
}