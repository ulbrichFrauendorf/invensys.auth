using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using invensys.auth.application.Common.Encryption;
using invensys.auth.application.Endpoints.AuthClients.Queries;
using invensys.auth.application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace invensys.auth.server.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController : ApiControllerBase
{
    [HttpPost]
    public async Task<IResult> Token([FromForm] AuthTokenRequest request)
    {
        if (request.grant_type == "client_credentials")
        {
            var client = await Mediator.Send(new GetAuthClientsQuery { ClientId = request.client_id });

            var dbClientSecret = EncryptionService.Encrypt(request.client_secret);
            var clientSecret = EncryptionService.Decrypt(client.SecretHash);
            if (request.client_secret != clientSecret)
            {
                return Results.Unauthorized();
            }
            
            var issuer = Configuration["Jwt:Issuer"];
            var audience = client.Url;
            var key = Encoding.ASCII.GetBytes(clientSecret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, ""),
                    new Claim(JwtRegisteredClaimNames.Email, ""),
                    new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString()),
                    new Claim("Scopes", request.scope)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return Results.Ok(new AuthTokenResponse { access_token = jwtToken, expires_in = 180 });
        }

        return Results.Unauthorized();
    }
}