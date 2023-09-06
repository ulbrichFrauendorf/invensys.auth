using invensys.auth.infrastructure.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invensys.auth.infrastructure
{
    public static class ServiceRestristrar
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AuthenticationServerConnection");
            _ = services.AddDbContext<AuthenticationServerContext>(options =>
            {
                _ = options.UseSqlServer(connectionString);
            });
        }
    }

    public static class BuilderService
    {
        public static void UseInfrastructure(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<AuthenticationServerContext>();
            dataContext.Database.Migrate();
        }
    }
}
