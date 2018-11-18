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
		PodioAccessToken _token = null;
		public PasswordAuthTokenProvider(string clientId, string clientSecret, string username, string password)
		{
			this.clientId = clientId;
			this.clientSecret = clientSecret;
			this.username = username;
			this.password = password;
		}
        
		public PodioAccessToken TokenData
        {
            get
            {
				if (_token == null)
					_token = auth();
				if (_token.Expiration < getEpoch())
					_token = refresh();
				return _token;
            }
        }

		private PodioAccessToken refresh()
		{
			var authRequest = new Dictionary<string, string>()
			{
				{"client_id",clientId},
				{"client_secret",clientSecret},
				{"refresh_token",_token.RefreshToken},
				{"grant_type", "refresh_token"}
			};

            var authClient = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.podio.com:443/oauth/token");
            requestMessage.Content = new FormUrlEncodedContent(authRequest);
            var response = authClient.SendAsync(requestMessage).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var podioOAuth = Newtonsoft.Json.JsonConvert.DeserializeObject<PodioOAuth>(responseString);

            var expiration = podioOAuth.ExpiresIn + getEpoch();
            _token = new PodioAccessToken
            {
                AccessToken = podioOAuth.AccessToken,
                Expiration = expiration,
                RefreshToken = podioOAuth.RefreshToken
            };
			return _token;
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

			var expiration = podioOAuth.ExpiresIn + getEpoch();
			_token = new PodioAccessToken
			{
				AccessToken = podioOAuth.AccessToken,
				Expiration = expiration,
                RefreshToken = podioOAuth.RefreshToken
			};
			return _token;
		}

		private int getEpoch()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch;
        }	
	}
}
