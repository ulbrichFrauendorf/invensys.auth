using AutoMapper;
using invensys.auth.domain;

namespace invensys.auth.application.Endpoints.AuthUsers;

public class AuthUserDTO
{
    public string AuthUserId { get; init; }
    public string UserName { get; init;}
    
    public string AccessToken { get; set;}
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AuthUser, AuthUserDTO>();
        }
    }
}