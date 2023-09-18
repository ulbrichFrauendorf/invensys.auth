namespace invensys.auth.application.Models;

public class ExternalApiRequest
{
    public string BaseUrl { get; set; }
    public string RequestUrl { get; set; }
    public string ApiSecret { get; set; }
}