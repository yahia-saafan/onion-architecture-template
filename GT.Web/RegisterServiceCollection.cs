using GT.Application.Common;
using GT.Core.Interfaces;
using GT.Infrastructure.Data;
using GT.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.Validators;
using GT.Application;
using GT.Application.Services.Engineers;

namespace GT.Web;

public static class RegisterServiceCollection
{
    /// <summary>
    /// Adds the infrastructure layer services.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The configuration instance.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Configure the database context
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            );
        });

        return services;
    }

    /// <summary>
    /// Adds the infrastructure layer services.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="configuration">The configuration instance.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        // Register validation helper
        services.AddScoped<IValidationHelper, ValidationHelper>();
        services.AddValidatorsFromAssembly(typeof(ApplicationReference).Assembly);

        // Register services
        services.AddScoped<IEngineerService, EngineerService>();

        return services;
    }
}
