using BakeryShop.Application.Common.Models;
using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        int pageNumber, int pageSize) where TDestination : IEntity<Guid>
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
}