using invensys.auth.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invensys.auth.infrastructure.Persistance
{
    public class AuthenticationServerContext: DbContext
    {
        public AuthenticationServerContext(DbContextOptions<AuthenticationServerContext> options)
            : base(options)
        {

        }

        public DbSet<AuthClient> AuthClients { get; set; }
    }
}
