using ProEShop.Services.Contracts;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace ProEShop.Services.Services;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService()
    {
        _httpClient = new HttpClient();
    }
    public async Task<HttpResponseMessage> SendAsync(string url, HttpMethod method,
        Dictionary<string, string> headers = default, string content = "", string mediaType = MediaTypeNames.Application.Json)
    {
        var request = new HttpRequestMessage
        {
            Method = method,
            RequestUri = new Uri(url),
            Content = new StringContent(content, Encoding.UTF8, mediaType),
        };
        if (headers != null)
            foreach (var header in headers)
                request.Headers.Add(header.Key, header.Value);
        return await _httpClient.SendAsync(request);
    }
}
