using System.Collections.Generic;
using System.Threading.Tasks;
using PodioCore.Models;
using PodioCore.Services;
namespace PodioCore.Contacts
{
    public static class ContactServiceMethods
    {
        private static ContactService _service;
        private static bool initialized;
        private static void init(Podio client)
        {
            _service = new ContactService(client);
            initialized = true;
        }
        
        public static async Task<Contact> GetContact(this Podio client, int userId)
        {
            if (!initialized)
                init(client);

            var result = await _service.GetUserContact(userId);
            return result;
        }       
    }
}
