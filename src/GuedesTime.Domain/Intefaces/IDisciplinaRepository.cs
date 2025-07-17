

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
	public interface IDisciplinaRepository : IRepository<Disciplina>
	{
		Task<Disciplina> ObterDisciplinaPorNome(Guid instituicaoId, string disciplinaNome);
		Task<(bool Existe, List<string> NomesExistentes)> VerificarDisciplinasExistentesPorNomes(Guid instituicaoId, List<string> nomes);
	}
}