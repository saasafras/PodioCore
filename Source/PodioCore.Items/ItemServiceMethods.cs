﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using PodioCore.Models;
using PodioCore.Utils;
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

		public static async Task<Item> GetItem(this Podio client, int itemId)
		{
			if (!initialized)
				init(client);

			var result = await _service.GetItemBasic(itemId, false);
			return result;
		}

		public static async Task<int> GetCurrentItemVersion(this Podio client, int itemId)
		{
			if (!initialized)
				init(client);

			var attributes = new Dictionary<string, string>
			{
				{"view","micro"},
				{"fields","current_revision"}
			};
			var url = $"/item/{itemId}";
			var result = await client.Get<dynamic>(url, attributes);
			int cr = result.current_revision.revision;
			return cr;
		}

		public static async Task<Item> GetAppItem(this Podio client, int appId, int appItemId)
        {
            if (!initialized)
                init(client);
            
            var result = await _service.GetItemByAppItemId(appId, appItemId);
            return result;
        }
        
		public static async Task<Item> GetFullItem(this Podio client, int itemId)
        {
            if (!initialized)
                init(client);
            
            var result = await _service.GetItem(itemId, false);
            return result;
        }
        
        public static async Task<int> CreateItem(this Podio client, Item item, int appId, bool hook)
        {
            if (!initialized)
                init(client);

            var result = await _service.AddNewItem(appId, item, silent: true, hook: hook);
            return result;
        }

        public static async Task<int> UpdateItem(this Podio client, Item item, bool hook)
        {
            if (!initialized)
                init(client);

            var result = await _service.UpdateItem(item, silent: true, hook: hook);
            if(!result.HasValue)
			{
				System.Console.WriteLine("revision_id was not returned. There was probably nothing to update.");
				System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
				return 0;
			}
            return result.Value;
        }

        public static async Task DeleteItem(this Podio client, int itemId, bool hook)
        {
            if (!initialized)
                init(client);
            
            await _service.DeleteItem(itemId, silent: true, hook: hook);
        }

        public static async Task<IEnumerable<ItemReference>> GetReferringItems(this Podio client, int itemId)
        {
            if (!initialized)
                init(client);

            var refs = await _service.GetItemReferences(itemId);
            return refs;
        }

        public static async Task<PodioCollection<Item>> FilterItems(this Podio client, int appId, Models.Request.FilterOptions filterOptions)
		{
			if (!initialized)
                init(client);

			var items = await _service.FilterItems(appId, filterOptions, false);
			return items;
		}
        
        public static async Task ActionOnFilterItems(this Podio client, int appId, Action<Item> action, Models.Request.FilterOptions filterOptions)
		{
			if (!initialized)
				init(client);

			await _service.FilterItemsForAction(appId, action, filterOptions);
		}

        public static async Task<List<ItemRevision>> GetItemRevisions(this Podio client, int itemId)
		{
			if (!initialized)
                init(client);
            
			var rev = await _service.GetItemRevisions(itemId);
			return rev;
		}

        public static async Task<List<ItemDiff>> GetRevisionDifference(this Podio client, int itemId, int from, int to)
		{
			if (!initialized)
                init(client);

			var diff = await _service.GetItemRevisionDifference(itemId, from, to);
			return diff;
		}
    }
}
