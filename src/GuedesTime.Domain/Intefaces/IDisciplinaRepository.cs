

using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IDisciplinaRepository : IRepository<Disciplina>
    {
        Task<bool> ObterDisciplinaPorNome(Guid instituicaoId, string disciplinaNome);
    }
}