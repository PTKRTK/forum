namespace ActiveForum.Server.Helpers
{
using System.Linq;
using ActiveForum.Shared.DTOs;

public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable.Skip((paginationDTO.CurrentPage - 1) * paginationDTO.PageObjectsNumber).Take(paginationDTO.PageObjectsNumber);
        }
    }
}
