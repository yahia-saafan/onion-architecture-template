using GT.Core;
using GT.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GT.Infrastructure.Extensions;

public static class AsyncIQueryableExtensions
{
    /// <summary>
    /// Paginates the given queryable source.
    /// </summary>
    /// <typeparam name="T">The type of the elements.</typeparam>
    /// <param name="source">The queryable source.</param>
    /// <param name="pageIndex">The page index (0-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paged list of entities.</returns>
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);

        //Ensure the minimum allowed page size is 1
        pageSize = Math.Max(pageSize, 1);

        //Handle negative values
        pageIndex = Math.Max(pageIndex, 0);

        var count = await source.CountAsync(cancellationToken);

        var data = await source
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
        .ToListAsync(cancellationToken);

        return new PagedList<T>(data, pageIndex, pageSize, count);
    }
}