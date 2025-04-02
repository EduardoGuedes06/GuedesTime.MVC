

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IFeriadoService : IDisposable
    {
        Task Adicionar(Feriado feriado);
        Task Atualizar(Feriado feriado);
        Task ObterPorId(Feriado feriado);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}