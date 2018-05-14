using System.Collections.Generic;
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
    }
}
