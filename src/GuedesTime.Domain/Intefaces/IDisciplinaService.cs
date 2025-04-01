

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IDisciplinaService : IDisposable
    {
        Task Adicionar(Disciplina disciplina);
        Task Atualizar(Disciplina disciplina);
        Task ObterPorId(Disciplina disciplina);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}