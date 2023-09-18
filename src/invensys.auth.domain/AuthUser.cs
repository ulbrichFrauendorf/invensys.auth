namespace invensys.auth.domain;

public class AuthUser
{
    public string AuthUserId { get; set; }
    public string UserName { get; set;}
    public string PasswordHash { get; set; }
}