using GuedesTime.Domain.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Intefaces.Utils
{
    public interface IPagedResultRepository<T> where T : class
    {
        Task<PagedResult<T>> GetPagedResultAsync(IQueryable<T> query, int pageSize, int? page = null);
    }

}
