using AutoMapper;
using AutoMapper.QueryableExtensions;
using invensys.auth.application.Common.Exceptions;
using MediatR;
using invensys.auth.application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace invensys.auth.application.Endpoints.AuthClients.Queries;

public record GetAuthClientsQuery : IRequest<AuthClientDto>
{
    public Guid ClientId { get; init; }
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
        return await _context.AuthClients
                   .ProjectTo<AuthClientDto>(_mapper.ConfigurationProvider)
                   .FirstOrDefaultAsync(s => s.Id == request.ClientId, cancellationToken) ??
               throw new NullQueryResultException();
    }
}