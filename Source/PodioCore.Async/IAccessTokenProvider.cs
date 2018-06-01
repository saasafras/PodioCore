using System;
namespace PodioCore
{
    public interface IAccessTokenProvider
    {
        PodioAccessToken TokenData { get; }
    }
}