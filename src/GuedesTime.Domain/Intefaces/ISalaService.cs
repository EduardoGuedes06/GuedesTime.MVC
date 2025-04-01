

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface ISalaService : IDisposable
    {
        Task Adicionar(Sala sala);
        Task Atualizar(Sala sala);
        Task ObterPorId(Sala sala);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}