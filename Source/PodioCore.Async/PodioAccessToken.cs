using System;
namespace PodioCore
{
    public class PodioAccessToken
    {
		public string AccessToken { get; set; }
		public int Expiration { get; set; }
		public string RefreshToken { get; set; }
    }
}