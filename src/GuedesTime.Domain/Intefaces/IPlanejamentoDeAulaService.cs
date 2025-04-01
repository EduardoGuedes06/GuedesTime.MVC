

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IPlanejamentoDeAulaService : IDisposable
    {
        Task Adicionar(PlanejamentoDeAula planejamentoDeAula);
        Task Atualizar(PlanejamentoDeAula planejamentoDeAula);
        Task ObterPorId(PlanejamentoDeAula planejamentoDeAula);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}