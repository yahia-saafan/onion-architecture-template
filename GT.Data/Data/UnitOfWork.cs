using GT.Core.Interfaces;

namespace GT.Infrastructure.Data;

/// <summary>
/// Implements the <see cref="IUnitOfWork"/> interface to manage database transactions using <see cref="AppDbContext"/>.
/// </summary>
/// <param name="context">The application's database context used for committing changes.</param>
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
