using AutoMapper;
using invensys.auth.application.Common.Interfaces;

namespace invensys.auth.application.Endpoints;

public abstract class EndpointHandler
{
    protected readonly IAuthenticationServerContext _context;
    protected readonly IMapper _mapper;

    protected EndpointHandler(IAuthenticationServerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}