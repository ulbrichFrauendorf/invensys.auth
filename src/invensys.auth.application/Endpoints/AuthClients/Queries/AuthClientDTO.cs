﻿using AutoMapper;
using invensys.auth.domain;

namespace invensys.auth.application.Endpoints.AuthClients.Queries;

public class AuthClientDto
{
    public string? AuthClientId { get; init; }
    public string? Name { get; init; }
    public string? SecretHash { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AuthClient, AuthClientDto>();
        }
    }
}