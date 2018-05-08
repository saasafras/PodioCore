using System.Net.Http;
namespace PodioCore
{
    public interface IRequestProcessor
    {
        HttpRequestMessage Process(HttpRequestMessage requestMessage);
    }
}
