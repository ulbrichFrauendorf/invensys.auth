using AutoMapper;
using invensys.auth.domain;

namespace invensys.auth.application.Endpoints.AuthUsers;

public class AuthUserDTO
{
    public string? AuthUserId { get; init; }
    public string? UserName { get; init;}
    
    public byte[]? PasswordHash { get; init;}
    
    public byte[]? PasswordSalt { get; init;}

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AuthUser, AuthUserDTO>();
        }
    }
}