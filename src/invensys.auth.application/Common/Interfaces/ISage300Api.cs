namespace invensys.auth.application.Common.Interfaces;

public interface ISage300Api
{
    Task<string> GetPayloadAsync(string baseUrl, string requestUrl, string apiKey);
    Task<string> PushPayloadAsync(string baseUrl, string requestUrl, string apiKey, string jsonRequestBody);
}