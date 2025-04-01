

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IRestricaoService : IDisposable
    {
        Task Adicionar(Restricao restricao);
        Task Atualizar(Restricao restricao);
        Task ObterPorId(Restricao restricao);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}