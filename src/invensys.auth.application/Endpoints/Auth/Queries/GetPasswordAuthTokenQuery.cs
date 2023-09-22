using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using invensys.auth.application.Common.Encryption;
using invensys.auth.application.Common.Exceptions;
using invensys.auth.application.Common.Interfaces;
using invensys.auth.application.Endpoints.AuthUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace invensys.auth.application.Endpoints.Auth.Queries;

public record GetPasswordAuthTokenQuery: IRequest<AuthUserDTO>
{
    [Required] public string UserName { get; init; }
    [Required] public string Password { get; init; }
}

public class GetPasswordAuthTokenQueryHandler : EndpointHandler, IRequestHandler<GetPasswordAuthTokenQuery, AuthUserDTO>
{
    private readonly IConfiguration _configuration;

    public GetPasswordAuthTokenQueryHandler(IAuthenticationServerContext context, IMapper mapper,
                                            IConfiguration configuration) : base(context, mapper)
    {
        _configuration = configuration;
    }

    public async Task<AuthUserDTO> Handle(GetPasswordAuthTokenQuery request, CancellationToken cancellationToken)
    {
        var authUser = await _context.AuthUsers.SingleOrDefaultAsync(s => s.UserName == request.UserName,
            cancellationToken: cancellationToken);
        if (authUser == null)
            throw new ForbiddenAccessException();

        using var hmac = new HMACSHA512(authUser.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != authUser.PasswordHash[i])
                throw new ForbiddenAccessException();
        }

        var mappedAuthUser = _mapper.Map<AuthUserDTO>(authUser);

        var clientSecret = _configuration["Jwt:Secret"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(clientSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, "auth.server.client"),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        mappedAuthUser.AccessToken = jwtToken;
        return mappedAuthUser;
    }
}