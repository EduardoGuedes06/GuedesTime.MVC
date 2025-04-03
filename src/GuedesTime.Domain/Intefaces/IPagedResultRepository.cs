

using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IPagedResultRepository<T> where T : class
    {
        Task<PagedResult<T>> GetPagedResult(IQueryable<T> query, int pageSize, int? page = null);
    }
}