using GT.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using GT.Core;
using GT.Infrastructure.Data;
using GT.Infrastructure.Extensions;

namespace GT.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected AppDbContext DbContext { get; init; }

    public Repository(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    #region Helper Methods

    protected IQueryable<TEntity> GetAllQuery(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();

        if (filter != null)
            query = query.Where(filter);

        return query;
    }
    protected IQueryable<TEntity> GetAllOrderedQuery<TSort>(Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, TSort>>? orderBy = null,
        bool isAscending = true)
    {
        var query = GetAllQuery(filter);

        if (orderBy != null)
            query = isAscending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

        return query;
    }

    #endregion

    #region Read

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();

        if (!trackChanges)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        var query = GetAllQuery(filter);

        if (!trackChanges)
            query = query.AsNoTracking();

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TResult>> GetAllProjectedAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        var query = GetAllQuery(filter);

        var finalQuery = query.Select(selector);

        return await finalQuery.ToListAsync(cancellationToken);
    }

    public async Task<IPagedList<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>>? filter = null,
        int pageIndex = 0,
        int pageSize = 20,
        bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        var query = GetAllQuery(filter);

        if (!trackChanges)
            query = query.AsNoTracking();

        return await query.ToPagedListAsync(pageIndex, pageSize, cancellationToken);
    }

    public async Task<IPagedList<TEntity>> GetAllPagedAsync<TSort>(Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, TSort>>? orderBy = null,
        bool isAscending = true,
        int pageIndex = 0,
        int pageSize = 20,
        bool trackChanges = false,
        CancellationToken cancellationToken = default)
    {
        var query = GetAllOrderedQuery(filter, orderBy, isAscending);

        if (!trackChanges)
            query = query.AsNoTracking();

        return await query.ToPagedListAsync(pageIndex, pageSize, cancellationToken);
    }


    public async Task<IPagedList<TResult>> GetAllPagedProjectedAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        int pageIndex = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        var query = GetAllQuery(filter);

        var finalQuery = query.Select(selector);

        return await finalQuery.ToPagedListAsync(pageIndex, pageSize, cancellationToken);
    }

    public async Task<IPagedList<TResult>> GetAllPagedProjectedAsync<TResult, TSort>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, TSort>>? orderBy = null,
        bool isAscending = true,
        int pageIndex = 0,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(selector);

        var query = GetAllOrderedQuery(filter, orderBy, isAscending);

        var finalQuery = query.Select(selector);

        return await finalQuery.ToPagedListAsync(pageIndex, pageSize, cancellationToken);
    }

    #endregion

    #region CREATE, UPDATE, DELETE

    public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entities);

        await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        DbContext.Set<TEntity>().Update(entity);
    }

    public void Update(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        DbContext.Set<TEntity>().UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is ISoftDeletedEntity softDeletedEntity)
        {
            softDeletedEntity.Deleted = true;
            DbContext.Set<TEntity>().Update(entity);
        }
        else
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
    }

    #endregion

}
