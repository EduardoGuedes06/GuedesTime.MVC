

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface ITurmaService : IDisposable
    {
        Task Adicionar(Turma turma);
        Task Atualizar(Turma turma);
        Task ObterPorId(Turma turma);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}