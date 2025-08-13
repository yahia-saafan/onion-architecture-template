namespace GT.Core.Interfaces;

/// <summary>
/// Defines a unit of work that manages transaction persistence.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Commits all changes made in the current unit of work to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous commit operation.</returns>
    Task CommitAsync(CancellationToken cancellationToken = default);
}
