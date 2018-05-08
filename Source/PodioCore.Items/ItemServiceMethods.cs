using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using PodioCore.Models;
using PodioCore.Services;
namespace PodioCore.Items
{
    public static class ItemServiceMethods
    {
        private static ItemService _service;
        private static bool initialized;
        private static void init(Podio client)
        {
            _service = new ItemService(client);
            initialized = true;
        }

        public static async Task<Item> Get(this Podio client, int itemId)
        {
            if (!initialized)
                init(client);
            
            var result = await _service.GetItem(itemId, false);
            return result;
        }

        public static async Task<int> Post(this Podio client, Item item, int appId)
        {
            if (!initialized)
                init(client);

            var result = await _service.AddNewItem(appId, item, silent: true, hook: false);
            return result;
        }

        public static async Task<int> PostWithEvent(this Podio client, Item item, int appId)
        {
            if (!initialized)
                init(client);

            var result = await _service.AddNewItem(appId, item, silent: true, hook: true);
            return result;
        }

        public static async Task<int> Put(this Podio client, Item item, int appId)
        {
            if (!initialized)
                init(client);

            var result = await _service.UpdateItem(item, silent: true, hook: false);
            return result.Value;
        }

        public static async Task<int> PutWithEvent(this Podio client, Item item, int appId)
        {
            if (!initialized)
                init(client);

            var result = await _service.UpdateItem(item, silent: true, hook: false);
            return result.Value;
        }

        public static async Task Delete(this Podio client, int itemId)
        {
            if (!initialized)
                init(client);

            await _service.DeleteItem(itemId, silent: true, hook: false);
        }

        public static async Task DeleteWithEvent(this Podio client, int itemId)
        {
            if (!initialized)
                init(client);

            await _service.DeleteItem(itemId, silent: true, hook: true);
        }

        public static async Task<IEnumerable<ItemReference>> GetReferringItems(this Podio client, int itemId)
        {
            if (!initialized)
                init(client);

            var refs = await _service.GetItemReferences(itemId);
            return refs;
        }
    }
}
