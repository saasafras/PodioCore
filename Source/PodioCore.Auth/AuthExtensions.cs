using System.Collections.Generic;
using System.Threading.Tasks;
using PodioCore.Auth;
using PodioCore.Utils.Authentication;
namespace PodioCore.Auth
{
    public static class AuthExtensions
    {
		public static async Task<PodioOAuth> AuthenticateWithPassword(this Podio client, string clientId, string clientSecret, string username, string password)
        {
			var authRequest = new Dictionary<string, string>()
			{
				{"client_id",clientId},
				{"client_secret",clientSecret},
				{"username", username},
				{"password", password},
				{"grant_type", "password"}
			};
			PodioOAuth podioOAuth = await client.Post<PodioOAuth>("/oauth/token", authRequest, true).ConfigureAwait(false);
            client.OAuth = podioOAuth;
            client.AuthStore.Set(podioOAuth);

            return podioOAuth;
        }
    }
}
