using GT.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GT.Infrastructure.Data;

/// <summary>
/// Represents the application's database context, providing access to the database using Entity Framework Core.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Engineer> Engineers { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to be used by the <see cref="DbContext"/>.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the model by applying all configurations from the assembly containing <see cref="InfrastructureReference"/>.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureReference).Assembly);
    }
}
