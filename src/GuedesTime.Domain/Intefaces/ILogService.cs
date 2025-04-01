

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface ILogService : IDisposable
    {
        Task Adicionar(Log log);
        Task Atualizar(Log log);
        Task ObterPorId(Log log);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}