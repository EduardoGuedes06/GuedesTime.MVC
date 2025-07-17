

using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;

namespace GuedesTime.Domain.Intefaces
{
	public interface ISerieRepository : IRepository<Serie>
	{
		Task<List<string>> ObterNomesSeriesDuplicadasAsync(Guid instituicaoId, List<string> nomesSeries, EnumTipoEnsino tipoEnsino, string? serieUnica = null, Guid? idSerie = null);
		Task<Serie> ObterSeriePorNome(Guid instituicaoId, string serieNome);
	}
}