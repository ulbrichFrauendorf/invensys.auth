using Microsoft.AspNetCore.Mvc;

namespace invensys.auth.server.Controllers;
public class AccountController : ApiControllerBase
{
    public IResult Register()
    {

        return Results.Ok();
    }
}
