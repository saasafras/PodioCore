﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PodioCore.Models;
using PodioCore.Services;
namespace PodioCore.Comments
{
    public static class CommentServiceMethods
    {
        private static CommentService _service;
        private static bool initialized;
        private static void init(Podio client)
        {
            _service = new CommentService(client);
            initialized = true;
        }

        public static async Task<Comment> GetComment(this Podio client, int commentId)
        {
            if (!initialized)
                init(client);

            var result = await _service.GetComment(commentId);
            return result;
        }
        
        public static async Task<int> CommentOnItem(this Podio client, string comment, int itemId, bool hook)
        {
            if (!initialized)
                init(client);
			
            var request = new Models.Request.CommentCreateUpdateRequest();
            request.Value = comment;
            var result = await _service.AddCommentToObject("item", itemId, request, silent: true, hook: hook);
            return result;
        }

        public static async Task<List<Comment>> GetCommentsOnItem(this Podio client, int itemId)
		{
			if (!initialized)
                init(client);

			var result = await _service.GetCommentsOnObject("item", itemId);
			return result;
		}
    }
}
