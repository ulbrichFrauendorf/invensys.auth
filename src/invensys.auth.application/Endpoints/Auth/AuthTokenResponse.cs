namespace invensys.auth.application.Endpoints.Auth;

public class AuthTokenResponse
{
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public string refresh_token { get; set; }
    public string scope { get; set; }
}