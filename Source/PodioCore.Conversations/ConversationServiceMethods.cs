using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using PodioCore.Models;
using PodioCore.Services;
namespace PodioCore.Conversations
{
	public static class ConversationServiceMethods
    {
		private static ConversationService _service;
        private static bool initialized;
        private static void init(Podio client)
        {
			_service = new ConversationService(client);
            initialized = true;
        }

        public static async Task SendMessage(this Podio client, string subject, string body, int userId)
        {
            if (!initialized)
                init(client);
			var request = new Models.Request.ConversationCreateRequest
			{
				Subject = subject,
				Text = body,
				Participants = new List<int> { userId }
			};
			var result = await _service.CreateConversation(request);
        }
    }
}
