using GuedesTime.Data.Context;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace GuedesTime.Data.Repository.Utils
{
    public class PagedResultRepository<T> : IPagedResultRepository<T> where T : class
    {
        public async Task<PagedResult<T>> GetPagedResultAsync(IQueryable<T> query, int pageSize, int? page = null, bool ativo = true)
        {
            pageSize = Math.Min(pageSize, 1000);
            var currentPage = page.HasValue && page.Value > 0 ? page.Value : 1;

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, totalCount, currentPage, pageSize);
        }
    }


}
