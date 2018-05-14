using System;
using System.Threading.Tasks;
using PodioCore.Services;
using PodioCore.Models;
namespace PodioCore.Spaces
{
    public static class SpaceServiceMethods
    {
		private static SpaceService _service;
        private static bool initialized;
        private static void init(Podio client)
        {
            _service = new SpaceService(client);
            initialized = true;
        }

        public static async Task<Space> GetSpace(this Podio client, int spaceId)
        {
            if (!initialized)
                init(client);

            var result = await _service.GetSpace(spaceId);
            return result;
        }
    }
}
