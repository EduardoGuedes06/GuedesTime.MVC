

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface ITarefasService : IDisposable
    {
        Task Adicionar(Tarefas tarefas);
        Task Atualizar(Tarefas tarefas);
        Task Remover(Guid id);
    }
}