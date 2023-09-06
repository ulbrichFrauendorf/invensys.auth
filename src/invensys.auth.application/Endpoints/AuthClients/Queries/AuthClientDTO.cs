using AutoMapper;
using invensys.auth.domain;

namespace invensys.auth.application.Endpoints.AuthClients.Queries;

public class AuthClientDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string SecretHash { get; init; } = null!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AuthClient, AuthClientDto>();
        }
    }
}