using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.Pagination;
using System.Linq.Expressions;

namespace POSIMSWebApi.QueryExtensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IQueryable<T> ToPaginatedResult<T>(
        this IQueryable<T> source,
        int? pageNumber,
        int? pageSize)
            {
                // Fallback to default values if null
                var currentPage = pageNumber ?? 1;
                var currentPageSize = pageSize ?? 10;

                // Apply pagination
                return source
                    .Skip((currentPage - 1) * currentPageSize)
                    .Take(currentPageSize);
            }

    }
}
