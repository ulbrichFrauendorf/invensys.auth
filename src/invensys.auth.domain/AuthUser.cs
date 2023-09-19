namespace invensys.auth.domain;

public class AuthUser
{
    public string AuthUserId { get; set; }
    public string UserName { get; set;}
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}