using invensys.auth.application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace invensys.auth.server.Controllers;
public class BugController : ApiControllerBase
{
    [HttpGet("bad-request")]
    public IResult BadRequest()
    {
        var exception = new NullReferenceException();

        var details = new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message,
            Status = 500
        };

        return Results.Problem(details);
    }
}
