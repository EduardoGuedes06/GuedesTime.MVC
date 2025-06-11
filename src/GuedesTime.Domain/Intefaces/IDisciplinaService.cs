

using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Utils;

namespace GuedesTime.Domain.Intefaces
{
    public interface IDisciplinaService : IDisposable
    {
        Task<PagedResult<Disciplina>> GetPagedByInstituicaoAsync(Guid instituicaoId, string? search, int page, int pageSize, bool ativo = true);
        Task Adicionar(Disciplina disciplina);
        Task AdicionarDisciplinas(Guid InstituicaoId, List<string> listaDeDisciplinas);
		Task Atualizar(Disciplina disciplina);
        Task<Disciplina> ObterPorId(Guid DisciplinaId);
        Task ObterTodos();
        Task Remover(Guid id);
        Task<Disciplina> ObterDisciplinaPorNome(Guid instituicaoId, string nomeDisciplina);
		Task<(bool Existe, List<string> NomesExistentes)> VerificarDisciplinasExistentesPorNomes(Guid instituicaoId, List<string> nomes);
	}
}