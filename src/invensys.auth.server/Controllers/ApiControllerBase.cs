using invensys.auth.server.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace invensys.auth.server.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    
    private IConfiguration? _configuration;
    protected IConfiguration Configuration =>
        _configuration ??= HttpContext.RequestServices.GetRequiredService<IConfiguration>();
}