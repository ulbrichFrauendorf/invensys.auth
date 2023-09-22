using AutoMapper;
using invensys.auth.application.Common.Interfaces;
using invensys.auth.domain;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;

namespace invensys.auth.application.Endpoints.AuthUsers.Commands;
public record CreateAuthUserCommand: IRequest<AuthUserDTO>
{
    [Required] public string UserName { get; init; }
    [Required] public string Password { get; init; }
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
            AuthUserId = Guid.NewGuid().ToString(),
            UserName = request.UserName,
            PasswordHash = hMac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hMac.Key
        };

        _context.AuthUsers.Add(authUser);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AuthUserDTO>(authUser);
    }
}

public class CreateTodoItemCommandValidator : AbstractValidator<CreateAuthUserCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(v => v.UserName)
            .MaximumLength(200)
            .EmailAddress()
            .NotEmpty();
    }
}