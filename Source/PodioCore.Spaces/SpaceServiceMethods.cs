using System;
using System.Collections.Generic;
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

        public static async Task<int> CreateSpace(this Podio client, int orgId, string name, string privacy, bool autoJoin, bool postOnNewApp, bool postOnNewMember)
		{
			if (!initialized)
				init(client);

			var spaceId = await _service.CreateSpace(orgId, name, privacy, autoJoin, postOnNewApp, postOnNewMember);
			return spaceId;
		}

		public static async Task<dynamic> UpdateSpace(this Podio client, int spaceId, string name = null, string urlLabel = null, string privacy = null, bool? autoJoin = null, bool? postOnNewApp = null, bool? postOnNewMember = null)
        {
            if (!initialized)
                init(client);

            var result = await _service.UpdateSpace(spaceId, name, urlLabel, privacy, autoJoin, postOnNewApp, postOnNewMember);
            return result;
        }

        public static async Task<List<SpaceMicro>> GetOrganizationSpaces(this Podio client, int orgId)
		{
			if (!initialized)
                init(client);

			var result = await _service.GetOrganizationSpaces(orgId);
			return result;
		}

		public static async Task<List<SpaceMicro>> GetAvailableSpaces(this Podio client, int orgId)
        {
            if (!initialized)
                init(client);

            var result = await _service.GetAvailableSpaces(orgId);
            return result;
        }
	}
}
