using invensys.auth.domain;
using Microsoft.EntityFrameworkCore;

namespace invensys.auth.application.Common.Interfaces;

public interface IAuthenticationServerContext
{
    DbSet<AuthClient> AuthClients { get; }
    DbSet<AuthUser> AuthUsers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}