using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using invensys.auth.application.Common.Exceptions;
using invensys.auth.application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invensys.auth.application.Endpoints.AuthUsers.Queries;

public record GetAuthUserByNameQuery: IRequest<AuthUserDTO>
{
    public string UserName { get; init; }
    public string Password { get; init; }
}

public class GetAuthUserByNameQueryHandler : EndpointHandler, IRequestHandler<GetAuthUserByNameQuery, AuthUserDTO>
{
    public GetAuthUserByNameQueryHandler(IAuthenticationServerContext context, IMapper mapper) 
        : base(context, mapper) { }
    
    public async Task<AuthUserDTO> Handle(GetAuthUserByNameQuery request, CancellationToken cancellationToken)
    {
        var authUser = await _context.AuthUsers.SingleOrDefaultAsync(s => s.UserName == request.UserName, cancellationToken: cancellationToken);
        if (authUser == null)
            throw new ForbiddenAccessException();

        using var hmac = new HMACSHA512(authUser.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != authUser.PasswordHash[i])
                throw new ForbiddenAccessException();
        }
        
        return _mapper.Map<AuthUserDTO>(authUser);
    }
}