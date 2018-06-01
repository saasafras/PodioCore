using System;
namespace PodioCore
{
    public interface IAccessTokenProvider
    {
        PodioAccessToken AccessToken { get; }
    }
}