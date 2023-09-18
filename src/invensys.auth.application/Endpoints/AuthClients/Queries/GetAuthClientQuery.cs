using AutoMapper;
using AutoMapper.QueryableExtensions;
using invensys.auth.application.Common.Exceptions;
using invensys.auth.application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invensys.auth.application.Endpoints.AuthClients.Queries;

public record GetAuthClientsQuery : IRequest<AuthClientDto>
{
    public string? ClientId { get; init; }
}

public class GetAuthClientQueryHandler : IRequestHandler<GetAuthClientsQuery, AuthClientDto>
{
    private readonly IAuthenticationServerContext _context;
    private readonly IMapper _mapper;

    public GetAuthClientQueryHandler(IAuthenticationServerContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuthClientDto> Handle(GetAuthClientsQuery request, CancellationToken cancellationToken)
    {
        var authClient = await _context.AuthClients
            .FirstOrDefaultAsync(s => s.AuthClientId == request.ClientId, cancellationToken);
        
        return _mapper.Map<AuthClientDto>(authClient);
    }
}