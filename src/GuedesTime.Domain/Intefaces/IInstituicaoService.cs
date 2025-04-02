

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IInstituicaoService : IDisposable
    {
        Task Adicionar(Instituicao instituicao);
        Task Atualizar(Instituicao instituicao);
        Task ObterPorId(Instituicao instituicao);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}