using PodioCore.Exceptions;
using PodioCore.Models;
using PodioCore.Services;
using PodioCore.Utils;
using PodioCore.Utils.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PodioCore
{
    public class Podio
    {
        public IAuthStore AuthStore { get; set; }
        public int RateLimit { get; private set; }
        public int RateLimitRemaining { get; private set; }
        public string ApiUrl { get; set; }

        private HttpClient HttpClient;
        private IAccessTokenProvider accessTokenProvider;
        IRequestProcessor requestProcessor;
        IResponseProcessor responseProcessor;

        public Podio(IAccessTokenProvider tokenProvider, string apiUrl = null, IRequestProcessor requestProcessor = null, IResponseProcessor responseProcessor = null)
        {
            accessTokenProvider = tokenProvider;
            this.requestProcessor = requestProcessor;
            this.responseProcessor = responseProcessor;
            ApiUrl = apiUrl ?? "https://api.podio.com:443";
            AuthStore = new NullAuthStore();
            HttpClient = new HttpClient();
        }

		public PodioAccessToken Token => accessTokenProvider.AccessToken;

		#region Request Helpers

		public async Task<T> Get<T>(string url, Dictionary<string, string> requestData = null, bool isFileDownload = false, bool returnAsString = false)
            where T : new()
        {
            string queryString = Utility.DictionaryToQueryString(requestData);
            if (!string.IsNullOrEmpty(queryString))
            {
                url = url + "?" + queryString;
            }

            var request = CreateHttpRequest(url, HttpMethod.Get, true, isFileDownload);
            return await Request<T>(request, isFileDownload, returnAsString);
        }

        public async Task<T> Post<T>(string url, dynamic requestData = null, bool isOAuthTokenRequest = false) where T : new()
        {
            var request = CreateHttpRequest(url, HttpMethod.Post, !isOAuthTokenRequest);
            if (isOAuthTokenRequest)
            {
                request.Content = new FormUrlEncodedContent(requestData);
            }
            else
            {
                var jsonString = JSONSerializer.Serilaize(requestData);
                request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            return await Request<T>(request);
        }

        public async Task<T> PostMultipartFormData<T>(string url, byte[] fileData, string fileName, string mimeType) where T : new()
        {
            var request = CreateHttpRequest(url, HttpMethod.Post);

            var multipartFormContent = new MultipartFormDataContent();
            multipartFormContent.Add(new ByteArrayContent(fileData), "source", fileName);
            multipartFormContent.Add(new StringContent(fileName), "filename");

            request.Content = multipartFormContent;

            return await Request<T>(request);
        }

        public async Task<T> Put<T>(string url, object requestData = null) where T : new()
        {
            var request = CreateHttpRequest(url, HttpMethod.Put);
            var jsonString = JSONSerializer.Serilaize(requestData);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            return await Request<T>(request);
        }

        public async Task<T> Delete<T>(string url, Dictionary<string, string> requestData = null) where T : new()
        {
            var request = CreateHttpRequest(url, HttpMethod.Delete);
            if(requestData != null)
                request.Content = new FormUrlEncodedContent(requestData);

            return await Request<T>(request);
        }

        public async Task<T> Request<T>(HttpRequestMessage httpRequest, bool isFileDownload = false, bool returnAsString = false) where T : new()
        {
            if (requestProcessor != null)
                httpRequest = requestProcessor.Process(httpRequest);
            
            // for retry in case of failure.
            var requestCopy = CreateHttpRequest(httpRequest.RequestUri.OriginalString, httpRequest.Method, true, isFileDownload);
            await Utility.CopyHttpRequestMessageContent(httpRequest, requestCopy);

            var response = await HttpClient.SendAsync(httpRequest);
            if (responseProcessor != null)
                response = responseProcessor.Process(response);
            
            // Get rate limits from header values
            if (response.Headers.Contains("X-Rate-Limit-Remaining"))
                RateLimitRemaining = int.Parse(response.Headers.GetValues("X-Rate-Limit-Remaining").First());
            if (response.Headers.Contains("X-Rate-Limit-Limit"))
                RateLimit = int.Parse(response.Headers.GetValues("X-Rate-Limit-Limit").First());

            if (response.IsSuccessStatusCode)
            {
                if(isFileDownload)
                {
                    var fileResponse = new FileResponse();
                    fileResponse.FileContents = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    fileResponse.ContentType = response.Content.Headers.ContentType.ToString();
                    fileResponse.ContentLength = response.Content.Headers.ContentLength ?? 0;

                    return fileResponse.ChangeType<T>();
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(T);
                    }

                    var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (returnAsString)
                    {
                        var stringResponse = new StringResponse { Data = responseBody };
                        return stringResponse.ChangeType<T>();
                    }

                    return JSONSerializer.Deserialize<T>(responseBody);
                }
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var podioError = JSONSerializer.Deserialize<PodioError>(responseBody);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new PodioAuthorizationException((int)response.StatusCode, podioError);
                }
                else
                {
                    ProcessErrorResponse(response.StatusCode, podioError);
                }

                return default(T);
            }
        }

        private HttpRequestMessage CreateHttpRequest(string url, HttpMethod httpMethod, bool addAuthorizationHeader = true, bool isFileDownload = false)
        {
            var fullUrl = ApiUrl + url;
            if (url.StartsWith("http")) fullUrl = url;

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(fullUrl),
                Method = httpMethod
            };

            if (isFileDownload)
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            else
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (addAuthorizationHeader)
            {
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenProvider.AccessToken.Token);
            }

            return request;
        }

        private void ProcessErrorResponse(HttpStatusCode statusCode, PodioError podioError)
        {
            var status = (int)statusCode;
            switch (status)
            {
                case 400:
                    if (podioError.Error == "invalid_grant")
                    {
                        throw new PodioInvalidGrantException(status, podioError);
                    }
                    else
                    {
                        throw new PodioBadRequestException(status, podioError);
                    }
                case 403:
                    throw new PodioForbiddenException(status, podioError);
                case 404:
                    throw new PodioNotFoundException(status, podioError);
                case 409:
                    throw new PodioConflictException(status, podioError);
                case 410:
                    throw new PodioGoneException(status, podioError);
                case 420:
                    throw new PodioRateLimitException(status, podioError);
                case 500:
                    throw new PodioServerException(status, podioError);
                case 502:
                case 503:
                case 504:
                    throw new PodioUnavailableException(status, podioError);
                default:
                    throw new PodioException(status, podioError);
            }
        }

        #endregion               
    }
}