using invensys.auth.application.Endpoints.AuthUsers;
using invensys.auth.application.Endpoints.AuthUsers.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace invensys.auth.server.Controllers;


public class AuthUsersController: ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuthUserDTO>>> GetUsers()
    {
        return await Mediator.Send(new GetAuthUsersQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthUserDTO>> GetUser(string id)
    {
        return await Mediator.Send(new GetAuthUserQuery() { AuthUserId = id });
    }
}