using AutoMapper;
using AutoMapper.QueryableExtensions;
using invensys.auth.application.Common.Exceptions;
using invensys.auth.application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invensys.auth.application.Endpoints.AuthUsers.Queries;

public record GetAuthUsersQuery: IRequest<List<AuthUserDTO>>
{
    
}

public class GetAuthUsersQueryHandler : EndpointHandler, IRequestHandler<GetAuthUsersQuery, List<AuthUserDTO>>
{
    public GetAuthUsersQueryHandler(IAuthenticationServerContext context, IMapper mapper) 
        : base(context, mapper) { }
    
    public async Task<List<AuthUserDTO>> Handle(GetAuthUsersQuery request, CancellationToken cancellationToken)
    {
        return await _context.AuthUsers
            .ProjectTo<AuthUserDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken)??
               throw new NullQueryResultException();
    }
}