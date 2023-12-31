﻿using invensys.auth.domain;
using Microsoft.EntityFrameworkCore;
using invensys.auth.application.Common.Interfaces;

namespace invensys.auth.infrastructure.Persistence;

public class AuthenticationServerContext: DbContext, IAuthenticationServerContext
{
    public AuthenticationServerContext(DbContextOptions<AuthenticationServerContext> options)
        : base(options)
    {

    }

    public required DbSet<AuthClient> AuthClients { get; set; }
    public required DbSet<AuthUser> AuthUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthClient>().Property(m => m.AuthClientId).IsRequired();
        modelBuilder.Entity<AuthClient>().Property(m => m.Url).IsRequired();
        modelBuilder.Entity<AuthClient>().Property(m => m.SecretHash).IsRequired();
        modelBuilder.Entity<AuthClient>().Property(m => m.Name).IsRequired();

        modelBuilder.Entity<AuthUser>().Property(m => m.AuthUserId).IsRequired();
        modelBuilder.Entity<AuthUser>().Property(m => m.UserName).IsRequired();
        
        base.OnModelCreating(modelBuilder);
    }
}