using AutoMapper;
using invensys.auth.application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invensys.auth.application.Endpoints.AuthUsers.Queries;

public record GetAuthUserQuery: IRequest<AuthUserDTO>
{
    public string AuthUserId { get; init; }
}

public class GetAuthUserQueryHandler : EndpointHandler, IRequestHandler<GetAuthUserQuery, AuthUserDTO>
{
    public GetAuthUserQueryHandler(IAuthenticationServerContext context, IMapper mapper) 
        : base(context, mapper) { }
    
    public async Task<AuthUserDTO> Handle(GetAuthUserQuery request, CancellationToken cancellationToken)
    {
        var authUser = await _context.AuthUsers.SingleOrDefaultAsync(s => s.AuthUserId == request.AuthUserId, cancellationToken: cancellationToken);
        return _mapper.Map<AuthUserDTO>(authUser);
    }
}