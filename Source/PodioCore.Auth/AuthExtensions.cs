using System.Collections.Generic;
using System.Threading.Tasks;
using PodioCore.Auth;
using System.Net.Http;
using PodioCore.Utils.Authentication;
using System;

namespace PodioCore.Auth
{
	public class PasswordAuthTokenProvider : IAccessTokenProvider
	{
		string clientId, clientSecret, username, password;
		public PodioAccessToken TokenData => new System.Lazy<PodioAccessToken>(
			() => auth()
		).Value;
        
		public PasswordAuthTokenProvider(string clientId, string clientSecret, string username, string password)
		{
			this.clientId = clientId;
			this.clientSecret = clientSecret;
			this.username = username;
			this.password = password;
		}
        
		private PodioAccessToken auth()
		{
			var authRequest = new Dictionary<string, string>()
			{
				{"client_id",clientId},
				{"client_secret",clientSecret},
				{"username", username},
				{"password", password},
				{"grant_type", "password"}
			};

			var authClient = new HttpClient();
			var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.podio.com:443/oauth/token");
			requestMessage.Content = new FormUrlEncodedContent(authRequest);
			var response = authClient.SendAsync(requestMessage).Result;
			var responseString = response.Content.ReadAsStringAsync().Result;
			var podioOAuth = Newtonsoft.Json.JsonConvert.DeserializeObject<PodioOAuth>(responseString);

			var expiration = int.Parse(podioOAuth.ExpiresIn) + getEpoch();
			return new PodioAccessToken
			{
				AccessToken = podioOAuth.AccessToken,
				Expiration = expiration,
                RefreshToken = podioOAuth.RefreshToken
			};
		}

		private int getEpoch()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch;
        }	
	}
}
