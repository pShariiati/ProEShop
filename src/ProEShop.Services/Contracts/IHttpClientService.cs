using System.Net.Mime;

namespace ProEShop.Services.Contracts;

public interface IHttpClientService
{
    Task<HttpResponseMessage> SendAsync(string url, HttpMethod method,
        Dictionary<string, string> headers = null,
        string content = "", string mediaType = MediaTypeNames.Application.Json);
}
