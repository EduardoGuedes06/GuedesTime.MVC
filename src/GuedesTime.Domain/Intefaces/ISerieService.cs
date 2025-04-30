

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface ISerieService : IDisposable
    {
        Task Adicionar(Serie serie);
        Task Atualizar(Serie serie);
        Task ObterPorId(Guid id);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}