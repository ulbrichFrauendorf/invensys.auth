using invensys.auth.application.Common.Interfaces;
using invensys.auth.application.Models;
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

    [HttpGet]
    public async Task<IResult> GetMappedResult([FromQuery] ExternalApiRequest externalApiRequest)
    {
        var response = await sage300Api.GetPayloadAsync(externalApiRequest.BaseUrl, externalApiRequest.RequestUrl, externalApiRequest.ApiSecret);
        return Results.Ok(response);
    }
}
