using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuedesTime.Data.Context;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.Data.Repository
{
    public class PagedResultRepository<T> : IPagedResultRepository<T> where T : class
    {
        private readonly MeuDbContext _context;

        public PagedResultRepository(MeuDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<T>> GetPagedResult(IQueryable<T> query, int pageSize, int? page = null)
        {
            if (pageSize > 1000)
            {
                pageSize = 1000;
            }

            if (page.HasValue && page.Value > 0)
            {
                var totalCount = await query.CountAsync();
                var items = await query
                    .Skip((page.Value - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedResult<T>(items, totalCount, page.Value, pageSize);
            }
            else
            {
                var totalCount = await query.CountAsync();
                var items = await query
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedResult<T>(items, totalCount, 1, pageSize);
            }
        }


    }
}