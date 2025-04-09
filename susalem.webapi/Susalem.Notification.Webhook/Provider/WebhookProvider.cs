using Susalem.Common.Extensions;
using Susalem.Common.Results;
using Susalem.Core.Application;
using Susalem.Core.Application.Interfaces.Providers;
using Susalem.Core.Application.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace Susalem.Notification.Webhook.Provider;

internal class WebhookProvider : IWebhookProvider
{
    private readonly ILogger<WebhookProvider> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private string _url;
    private HttpClient? _httpClient;

    public WebhookProvider(ILogger<WebhookProvider> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Result> PostDataAsync<T>(T data, string url, string requestUri)
    {
        var result = new Result();
        try
        {
            if (!string.IsNullOrEmpty(_url) && !_url.Equals(url))
            {
                _httpClient?.Dispose();
                _httpClient = null;
            }
            _url = url;
            _httpClient = _httpClientFactory.CreateClient("webhookClient");
            _httpClient.BaseAddress = new Uri(url);

            var requestBody = new StringContent(
                              JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var httpResult = await _httpClient.PostAsync(requestUri, requestBody);
            if (httpResult.StatusCode == System.Net.HttpStatusCode.OK ||
                httpResult.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                result.Successful();
                return result;
            }
            throw new InvalidOperationException(httpResult.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            ServicesHelper.HandleServiceError(ref result, _logger, ex, $"Webhook: {ex.Message}");
        }
        
        return result;
    }
}
