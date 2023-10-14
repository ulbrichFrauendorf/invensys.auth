using AutoMapper;
using FluentValidation;
using invensys.auth.application.Common.Exceptions;
using invensys.auth.application.Common.Interfaces;
using invensys.auth.domain;
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

        if (authUser == null)
        {
            throw new NotFoundException(nameof(AuthUser),request.AuthUserId);
        }
        
        return _mapper.Map<AuthUserDTO>(authUser);
    }
}

public class GetAuthUserQueryValidator : AbstractValidator<GetAuthUserQuery>
{
    public GetAuthUserQueryValidator()
    {
        RuleFor(v => v.AuthUserId)
            .Must(x=> Guid.TryParse(x, out _))
            .NotEmpty();
    }
}