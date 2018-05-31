using System.Collections.Generic;
using System.Threading.Tasks;
using PodioCore.Auth;
using System.Net.Http;
using PodioCore.Utils.Authentication;
namespace PodioCore.Auth
{
	public class PasswordAuthTokenProvider : IAccessTokenProvider
	{
		string clientId, clientSecret, username, password;
		public string AccessToken => new System.Lazy<string>(
			() => auth()
		).Value;

		public PasswordAuthTokenProvider(string clientId, string clientSecret, string username, string password)
		{
			this.clientId = clientId;
			this.clientSecret = clientSecret;
			this.username = username;
			this.password = password;
		}
        
		private string auth()
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

			return podioOAuth.AccessToken;
		}    
	}
}
