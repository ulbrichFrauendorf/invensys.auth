namespace invensys.auth.domain;

public class AuthClient
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SecretHash { get; set; }
    public string AllowedScopes { get; set; }
}