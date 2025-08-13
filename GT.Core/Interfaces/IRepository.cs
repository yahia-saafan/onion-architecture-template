using System.Linq.Expressions;

namespace GT.Core.Interfaces;

/// <summary>
/// Represents a generic repository interface
/// </summary>
/// <typeparam name="TEntity">The type of the entity being managed by the repository.</typeparam>
public interface IRepository<TEntity> 
    where TEntity : BaseEntity
{
    /// <summary>
    /// Retrieves an entity by its primary key.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task result contains the entity if found; otherwise, null.
    /// </returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the first entity that matches the specified filter condition, or null if no match is found.
    /// </summary>
    /// <param name="filter">A predicate to filter the entities.</param>
    /// <param name="trackChanges">Specifies whether to track entity changes in the EF Core context. Default is false.</param>
    /// <param name="cancellationToken">Allows the operation to be canceled.</param>
    /// <returns>A task that resolves to the first matching entity or null if no match is found.</returns>
    Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> filter,
        bool trackChanges = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an enumerable collection of entity entries, optionally filtered by a predicate.
    /// </summary>
    /// <param name="filter">An optional filter expression to apply to the query. If null, all entities are retrieved.</param>
    /// <param name="trackChanges">Specifies whether to track the retrieved entities in the current DbContext. Default is false.</param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// The task result contains an enumerable collection of matching entity entries.
    /// </returns>
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool trackChanges = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a projected collection of entity entries, 
    /// applying an optional filter and tracking behavior.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="selector">A projection expression to transform the entity into the desired result type.</param>
    /// <param name="filter">An optional filter expression to apply to the query. If null, all entities are considered.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task representing the asynchronous operation. 
    /// The task result contains an enumerable collection of projected entity entries.
    /// </returns>
    Task<IEnumerable<TResult>> GetAllProjectedAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of entities of type <typeparamref name="TEntity"/> based on the provided filter condition.
    /// The results are returned in a paged format without any specific sorting applied.
    /// </summary>
    /// <param name="filter">An optional predicate to filter the entities.</param>
    /// <param name="pageIndex">The zero-based index of the page to retrieve.</param>
    /// <param name="pageSize">The number of items per page. Default is <see cref="int.MaxValue"/> to return all items.</param>
    /// <param name="trackChanges">Specifies whether to track entity changes in the EF Core context. Default is false for read-only operations.</param>
    /// <param name="cancellationToken">Allows the operation to be canceled.</param>
    /// <returns>A task that resolves to a paginated list of entities.</returns>
    Task<IPagedList<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>>? filter = null,
        int pageIndex = 0, 
        int pageSize = 20, 
        bool trackChanges = false, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of entities of type <typeparamref name="TEntity"/> with optional filtering and sorting.
    /// Sorting can be applied dynamically based on the specified property.
    /// </summary>
    /// <param name="filter">An optional predicate to filter the entities.</param>
    /// <param name="orderBy">An optional expression specifying the property to sort by.</param>
    /// <param name="isAscending">Indicates whether sorting should be in ascending (true) or descending (false) order. Default is true.</param>
    /// <param name="pageIndex">The zero-based index of the page to retrieve.</param>
    /// <param name="pageSize">The number of items per page. Default is <see cref="int.MaxValue"/> to return all items.</param>
    /// <param name="trackChanges">Specifies whether to track entity changes in the EF Core context. Default is false for read-only operations.</param>
    /// <param name="cancellationToken">Allows the operation to be canceled.</param>
    /// <returns>A task that resolves to a paginated list of entities.</returns>
    Task<IPagedList<TEntity>> GetAllPagedAsync<TSort>(Expression<Func<TEntity, bool>>? filter = null, 
        Expression<Func<TEntity, TSort>>? orderBy = null,
        bool isAscending = true,
        int pageIndex = 0, 
        int pageSize = 20, 
        bool trackChanges = false, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a projected paged collection of entity entries, 
    /// applying an optional filter and tracking behavior.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="selector">A projection expression to transform the entity into the desired result type.</param>
    /// <param name="filter">An optional filter expression to apply to the query. If null, all entities are considered.</param>
    /// /// <param name="pageIndex">The zero-based index of the page to retrieve.</param>
    /// <param name="pageSize">The number of items per page. Default is <see cref="int.MaxValue"/> to return all items.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task representing the asynchronous operation. 
    /// The task result contains a paged collection of projected entity entries.
    /// </returns>
    Task<IPagedList<TResult>> GetAllPagedProjectedAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        int pageIndex = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a projected paged collection of entity entries, 
    /// applying an optional filter and tracking behavior.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="selector">A projection expression to transform the entity into the desired result type.</param>
    /// <param name="filter">An optional filter expression to apply to the query. If null, all entities are considered.</param>
    /// /// <param name="orderBy">An optional expression specifying the property to sort by.</param>
    /// <param name="isAscending">Indicates whether sorting should be in ascending (true) or descending (false) order. Default is true.</param>
    /// <param name="pageIndex">The zero-based index of the page to retrieve.</param>
    /// <param name="pageSize">The number of items per page. Default is <see cref="int.MaxValue"/> to return all items.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task representing the asynchronous operation. 
    /// The task result contains a paged collection of projected entity entries.
    /// </returns>
    Task<IPagedList<TResult>> GetAllPagedProjectedAsync<TResult, TSort>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, TSort>>? orderBy = null,
        bool isAscending = true,
        int pageIndex = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Insert the entity entry
    /// </summary>
    /// <param name="entity">Entity entry</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Insert entity entries
    /// </summary>
    /// <param name="entities">Entity entries</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified entity in the database.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    void Update(TEntity entity);

    /// <summary>
    /// Updates multiple entities in the database.
    /// </summary>
    /// <param name="entities">A collection of entities to be updated.</param>
    void Update(IEnumerable<TEntity> entities);

    /// <summary>
    /// Deletes the specified entity from the database. 
    /// If <paramref name="hardDelete"/> is false, the entity is soft-deleted instead of being permanently removed.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    /// <param name="hardDelete">Specifies whether the entity should be permanently removed (hard delete) or just marked as deleted (soft delete).</param>
    void Delete(TEntity entity);

}
