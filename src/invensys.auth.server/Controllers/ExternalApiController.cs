using invensys.auth.application.Models;
using invensys.auth.infrastructure.ExternalApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace invensys.auth.server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ExternalApiController : Controller
{
    private readonly ISage300Api sage300Api;

    public ExternalApiController(ISage300Api sage300Api)
    {
        this.sage300Api = sage300Api;
    }

    public async Task<IResult> GetMappedResult([FromQuery] ExternalApiRequest externalApiRequest)
    {
        var response = await sage300Api.GetPayloadAsync(externalApiRequest.BaseUrl, externalApiRequest.RequestUrl, externalApiRequest.ApiSecret);
        return Results.Ok(response);
    }
}
