using System.Security.Cryptography;
using System.Text;
using invensys.auth.application.Endpoints.Auth.Queries;
using invensys.auth.application.Endpoints.AuthUsers;
using invensys.auth.application.Endpoints.AuthUsers.Commands;
using invensys.auth.application.Endpoints.AuthUsers.Queries;
using Microsoft.AspNetCore.Mvc;

namespace invensys.auth.server.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpPost("token")]
    public async Task<IResult> Token([FromForm] GetAuthTokenQuery authTokenQuery)
    {
        var authTokenResponse = await Mediator.Send(authTokenQuery);
        if (!string.IsNullOrWhiteSpace(authTokenResponse.access_token))
            return Results.Ok(authTokenResponse);

        return Results.Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthUserDTO>> Register(CreateAuthUserCommand createAuthUserCommand)
    {
        if ((await Mediator.Send(new GetAuthUsersQuery())).Any(s => s.UserName == createAuthUserCommand.UserName))
            return BadRequest($"Account for {createAuthUserCommand.UserName} has already been created");

        return await Mediator.Send(createAuthUserCommand);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthUserDTO>> Login(GetAuthUserByNameQuery getAuthUserByNameQuery)
    {
        var user = await Mediator.Send(getAuthUserByNameQuery);

        if (user == null)
            return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(getAuthUserByNameQuery.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid password");
        }

        return user;
    }
}