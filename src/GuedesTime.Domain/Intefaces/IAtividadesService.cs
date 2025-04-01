

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IAtividadesService : IDisposable
    {
        Task Adicionar(Atividades atividades);
        Task Atualizar(Atividades atividades);
        Task ObterPorId(Atividades atividades);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}