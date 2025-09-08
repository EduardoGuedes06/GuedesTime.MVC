

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
	public interface IDisciplinaRepository : IRepository<Disciplina>
	{
		Task<Disciplina> ObterDisciplinaPorNome(Guid instituicaoId, string disciplinaNome);
        Task<List<string>> ObterNomesDisciplinasDuplicadasAsync(Guid instituicaoId, List<string> nomesDisciplinas, Guid? idDisciplina = null);
        Task<(bool Existe, List<string> NomesExistentes)> VerificarDisciplinasExistentesPorNomes(Guid instituicaoId, List<string> nomes);
	}
}