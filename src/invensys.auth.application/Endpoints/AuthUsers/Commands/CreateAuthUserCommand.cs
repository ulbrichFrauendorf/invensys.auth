using AutoMapper;
using invensys.auth.application.Common.Interfaces;
using invensys.auth.domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace invensys.auth.application.Endpoints.AuthUsers.Commands;
public record CreateAuthUserCommand: IRequest<AuthUserDTO>
{
    public string? UserName { get; init; }
    public string? Password { get; init; }
}

public class CreateAuthUserCommandHandler : EndpointHandler, IRequestHandler<CreateAuthUserCommand, AuthUserDTO>
{
    public CreateAuthUserCommandHandler(IAuthenticationServerContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<AuthUserDTO> Handle(CreateAuthUserCommand request, CancellationToken cancellationToken)
    {
        using var hMac = new HMACSHA512();

        var authUser = new AuthUser
        {
            AuthUserId = new Guid().ToString(),
            UserName = request.UserName,
            PasswordHash = hMac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hMac.Key
        };

        _context.AuthUsers.Add(authUser);
        await _context.SaveChangesAsync();

        return _mapper.Map<AuthUserDTO>(authUser);
    }
}