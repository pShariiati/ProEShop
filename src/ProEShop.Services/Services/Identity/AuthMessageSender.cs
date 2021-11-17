using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProEShop.Services.Contracts;
using ProEShop.Services.Contracts.Identity;
using ProEShop.Services.Services.Identity.Sms;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.Services.Services.Identity;

public class AuthMessageSender : ISmsSender
{
    #region Constructor
    private readonly SmsInfo _smsInfo;
    private readonly IHttpClientService _httpClient;

    public AuthMessageSender(
        IOptionsMonitor<SiteSettings> siteSettings,
        IHttpClientService httpClient)
    {
        _smsInfo = siteSettings.CurrentValue.SmsInfo;
        _httpClient = httpClient;
    }

    #endregion
    public async Task<bool> SendSmsAsync(string number, string message)
    {
        var token = await GetToken();
        var lineToSend = await GetLineToSendAsync(token);
        return await Send(token, message, number, lineToSend);
    }
    private async Task<string> GetToken()
    {
        var modelInJson = JsonConvert.SerializeObject(_smsInfo);
        var result = await _httpClient.SendAsync(
            "https://RestfulSms.com/api/Token",
            HttpMethod.Post,
            content: modelInJson
        );
        var responseBody = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SmsToken>(responseBody)
            .TokenKey;
    }

    private async Task<string> GetLineToSendAsync(string token)
    {
        var result = await _httpClient.SendAsync(
            "https://RestfulSms.com/api/SMSLine",
            HttpMethod.Get,
            new Dictionary<string, string>
            {
                {"x-sms-ir-secure-token", token }
            }
        );
        var responseBody = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SmsLineNumber>(responseBody)
            .SMSLines?.Last().LineNumber.ToString() ?? string.Empty;
    }

    private async Task<bool> Send(string token, string message, string number, string lineToSend)
    {
        var body = new
        {
            Messages = new List<string> { message },
            MobileNumbers = new List<string> { number },
            LineNumber = lineToSend,
            CanContinueInCaseOfError = false
        };
        var modelInJson = JsonConvert.SerializeObject(body);
        var result = await _httpClient.SendAsync(
            "https://RestfulSms.com/api/MessageSend",
            HttpMethod.Post,
            new Dictionary<string, string>
            {
                {"x-sms-ir-secure-token", token }
            },
            content: modelInJson
        );
        var responseBody = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SendSmsResult>(responseBody)
            .IsSuccessful;
    }
}