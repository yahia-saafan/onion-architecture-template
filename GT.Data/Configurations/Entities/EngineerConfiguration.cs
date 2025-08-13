using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GT.Core.Entities;

namespace GT.Infrastructure.Configurations.Entities;

/// <summary>
/// Provides configuration for the <see cref="Engineer"/> entity using the Entity Framework Core Fluent API.
/// </summary>
internal sealed class GovernorateConfiguration : IEntityTypeConfiguration<Engineer>
{
    public void Configure(EntityTypeBuilder<Engineer> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(e => e.NameAR)
            .HasMaxLength(100)
            .IsRequired(true);
    }
}
