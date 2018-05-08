using System.Net.Http;
namespace PodioCore
{
    public interface IResponseProcessor
    {
        HttpResponseMessage Process(HttpResponseMessage responseMessage);
    }
}
