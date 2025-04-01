

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IHorarioService : IDisposable
    {
        Task Adicionar(Horario horario);
        Task Atualizar(Horario horario);
        Task ObterPorId(Horario horario);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}