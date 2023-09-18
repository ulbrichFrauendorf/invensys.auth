using invensys.auth.application.Common.Interfaces;
using invensys.auth.infrastructure.ExternalApi;
using invensys.auth.infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace invensys.auth.infrastructure;

public static class ServiceRestristrar
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AuthenticationServerConnection");
        _ = services.AddDbContext<AuthenticationServerContext>(options =>
        {
            _ = options.UseSqlServer(connectionString);
        });

        _ = services.AddScoped<IAuthenticationServerContext>(provider => provider.GetRequiredService<AuthenticationServerContext>());

        _ = services.AddHttpClient();
        _ = services.AddSingleton<ISage300Api, Sage300Api>();
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