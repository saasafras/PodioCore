using System.Collections.Generic;
using System.Threading.Tasks;
using PodioCore.Auth;
using System.Net.Http;
using PodioCore.Utils.Authentication;
using System;
namespace PodioCore.Auth
{
	public class OneUseAccessTokenProvider : IAccessTokenProvider
    {
        public OneUseAccessTokenProvider(string accessToken)
        {
            _accessToken = accessToken;
        }
        private string _accessToken;

        public PodioAccessToken TokenData => new PodioAccessToken { AccessToken = _accessToken };
    }
}
