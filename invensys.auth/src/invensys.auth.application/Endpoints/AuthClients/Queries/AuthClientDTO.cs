using AutoMapper;
using invensys.auth.domain;

namespace invensys.auth.application.Endpoints.AuthClients
{
    public record AuthClientDTO
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public string? SecretHash { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<AuthClient, AuthClientDTO>();
            }
        }
    }
}