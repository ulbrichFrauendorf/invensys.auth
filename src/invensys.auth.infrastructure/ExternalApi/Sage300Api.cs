using invensys.auth.application.Common.Interfaces;

namespace invensys.auth.infrastructure.ExternalApi;


public class Sage300Api : ISage300Api
{
    private HttpClient client;
    private readonly IHttpClientFactory httpClientFactory;

    public Sage300Api(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetPayloadAsync(string baseUrl, string requestUrl, string apiKey)
    {
        client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(baseUrl);
        await SetToken(apiKey);

        var response = await client.PostAsync(requestUrl, null);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed with status {response.StatusCode} : Message from server: {await response.Content.ReadAsStringAsync()}");
        }

        var result = await response.Content.ReadAsStringAsync();
        client.Dispose();
        return result;
    }

    public async Task<string> PushPayloadAsync(string baseUrl, string requestUrl, string apiKey, string jsonRequestBody)
    {
        client = httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(baseUrl);
        await SetToken(apiKey);
        var content = new StringContent(jsonRequestBody, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync(requestUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed with status {response.StatusCode} : Message from server: {await response.Content.ReadAsStringAsync()}");
        }

        var result = await response.Content.ReadAsStringAsync();
        client.Dispose();
        return result;
    }

    private async Task SetToken(string apiKey)
    {
        var tokenRequest = $"grant_type=password&scope=apiKey={apiKey}";
        var content = new StringContent(tokenRequest, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await client.PostAsync("token", content);
        var responseStr = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed with status {response.StatusCode}");
        }

        if (!response.Headers.TryGetValues("set-cookie", out var values))
        {
            throw new HttpRequestException($"Failed to get cookie token.");
        }

        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Cookie", values.First());
    }
}