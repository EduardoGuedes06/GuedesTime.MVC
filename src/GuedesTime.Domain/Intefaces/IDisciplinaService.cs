

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IDisciplinaService : IDisposable
    {
        Task Adicionar(Disciplina disciplina);
        Task Atualizar(Disciplina disciplina);
        Task<Disciplina> ObterPorId(Guid DisciplinaId);
        Task<PagedResult<Disciplina>> GetPagedByInstituicaoAsync(Guid instituicaoId, string? search, int page, int pageSize, bool ativo = true);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}