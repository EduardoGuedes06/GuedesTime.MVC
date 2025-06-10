

using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Utils;

namespace GuedesTime.Domain.Intefaces
{
    public interface ISerieService : IDisposable
    {
        Task<PagedResult<Serie>> GetPagedByInstituicaoAsync(Guid instituicaoId, string? search, int page, int pageSize, bool ativo = true);
        Task Adicionar(Serie serie);
        Task Atualizar(Serie serie);
        Task<Serie> ObterPorId(Guid id);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}