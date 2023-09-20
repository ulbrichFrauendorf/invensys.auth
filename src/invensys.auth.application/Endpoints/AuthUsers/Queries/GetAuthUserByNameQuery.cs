using AutoMapper;
using invensys.auth.application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invensys.auth.application.Endpoints.AuthUsers.Queries;

public record GetAuthUserByNameQuery: IRequest<AuthUserDTO>
{
    public string? UserName { get; }
    public string? Password { get; init; }
}

public class GetAuthUserByNameQueryHandler : EndpointHandler, IRequestHandler<GetAuthUserByNameQuery, AuthUserDTO>
{
    public GetAuthUserByNameQueryHandler(IAuthenticationServerContext context, IMapper mapper) 
        : base(context, mapper) { }
    
    public async Task<AuthUserDTO> Handle(GetAuthUserByNameQuery request, CancellationToken cancellationToken)
    {
        var authUser = await _context.AuthUsers.SingleOrDefaultAsync(s => s.UserName == request.UserName, cancellationToken: cancellationToken);
        return _mapper.Map<AuthUserDTO>(authUser);
    }
}