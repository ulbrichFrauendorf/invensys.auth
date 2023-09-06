using invensys.auth.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using invensys.auth.application.Common.Interfaces;

namespace invensys.auth.infrastructure.Persistence;

public class AuthenticationServerContext: DbContext, IAuthenticationServerContext
{
    public AuthenticationServerContext(DbContextOptions<AuthenticationServerContext> options)
        : base(options)
    {

    }

    public DbSet<AuthClient> AuthClients { get; set; }
}