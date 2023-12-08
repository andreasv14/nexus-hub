using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NexusHub.Application.Features.Account.Services;
using NexusHub.Application.Services;
using NexusHub.Domain.CompanyContext;
using NexusHub.Infrastructure.Identity;
using NexusHub.Infrastructure.Identity.Models;
using NexusHub.Infrastructure.Persistence;
using NexusHub.Infrastructure.Persistence.Repositories;
using NexusHub.Infrastructure.Services;
using System.Text;

namespace NexusHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAuth(configuration)
            .AddPersistence(configuration)
            .AddServices()
            .AddAutoMapper(typeof(DependencyInjection).Assembly);
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("LocalDB"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        //services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        // Register repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        // For Identity
        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddTransient<IIdentityService, IdentityService>();

        services
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}