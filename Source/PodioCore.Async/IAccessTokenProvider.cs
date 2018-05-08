using System;
namespace PodioCore
{
    public interface IAccessTokenProvider
    {
        string AccessToken { get; }
    }
}
