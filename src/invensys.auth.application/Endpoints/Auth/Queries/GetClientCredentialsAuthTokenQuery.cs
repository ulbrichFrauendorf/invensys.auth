using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using invensys.auth.application.Common.Encryption;
using invensys.auth.application.Common.Exceptions;
using invensys.auth.application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace invensys.auth.application.Endpoints.Auth.Queries;

public record GetClientCredentialsAuthTokenQuery: IRequest<AuthTokenResponse>
{
    [Required] public string grant_type { get; init; }
    public string scope { get; init; }
    [Required] public string client_id { get; init; }
    [Required] public string client_secret { get; init; }
}

public class GetClientCredentialsAuthTokenQueryHandler : EndpointHandler, IRequestHandler<GetClientCredentialsAuthTokenQuery, AuthTokenResponse>
{
    private readonly IConfiguration _configuration;

    public GetClientCredentialsAuthTokenQueryHandler(IAuthenticationServerContext context, IMapper mapper, IConfiguration configuration) : base(context, mapper)
    {
        _configuration = configuration;
    }

    public async Task<AuthTokenResponse> Handle(GetClientCredentialsAuthTokenQuery request, CancellationToken cancellationToken)
    {
        switch (request.grant_type)
        {
            case "client_credentials":
            {
                var client = await _context.AuthClients
                    .FirstOrDefaultAsync(s => s.AuthClientId == request.client_id, cancellationToken);

                var clientSecret = EncryptionService.Decrypt(client.SecretHash);
                if (request.client_secret != clientSecret)
                {
                    throw new ForbiddenAccessException();
                }

                var issuer = _configuration["Jwt:Issuer"];
                var audience = client.Url;
                var key = Encoding.ASCII.GetBytes(clientSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, client.AuthClientId),
                        new Claim(JwtRegisteredClaimNames.Name, client.Name),
                        new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()),
                        new Claim("Scopes", request.scope)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return new AuthTokenResponse { access_token = jwtToken, expires_in = 180 };
            }
            default:
                return new AuthTokenResponse();;
        }
    }
}