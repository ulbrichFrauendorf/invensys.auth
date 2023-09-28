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
    public async Task<IResult> Token([FromForm] GetClientCredentialsAuthTokenQuery authTokenQuery)
    {
        var authTokenResponse = await Mediator.Send(authTokenQuery);
        return !string.IsNullOrWhiteSpace(authTokenResponse.access_token) ? Results.Ok(authTokenResponse) : Results.Unauthorized();
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthUserDTO>> Register(CreateAuthUserCommand createAuthUserCommand)
    {
        if ((await Mediator.Send(new GetAuthUsersQuery())).Any(s => s.UserName == createAuthUserCommand.UserName))
            return BadRequest($"Account for {createAuthUserCommand.UserName} has already been created");

        return await Mediator.Send(createAuthUserCommand);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthUserDTO>> Login(GetPasswordAuthTokenQuery getAuthUserByNameQuery)
    {
        return await Mediator.Send(getAuthUserByNameQuery);
    }
}