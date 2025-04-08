

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IProfessorService : IDisposable
    {
        Task Adicionar(Professor professor);
        Task Atualizar(Professor professor);
        Task<Professor> ObterPorId(Guid professorId);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}