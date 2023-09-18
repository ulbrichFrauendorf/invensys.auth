namespace invensys.auth.domain;

public class AuthClient
{
    public string AuthClientId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string SecretHash { get; set; }
    public string AllowedScopes { get; set; }
}