namespace ActiveForum.Server.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public static class PaginationHelpers
    {
        public static async Task<double> CountPagesAmount<T>(IQueryable<T> queryable, int pageObjectNumber)
        {
            double count = await queryable.CountAsync();
            double pagesAmount = Math.Ceiling(count / pageObjectNumber);
            return pagesAmount;
        }
    }
}
