using GuedesTime.Domain.Models.Utils;
using GuedesTime.Service.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IPagedResultRepository<T> where T : class
    {
        Task<PagedResult<T>> GetPagedResultAsync(IQueryable<T> query, int pageSize, int? page = null, bool ativo = true);
    }
}
