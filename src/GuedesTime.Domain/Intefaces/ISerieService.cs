using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;
using GuedesTime.Domain.Models.Utils;
using System.Linq.Expressions;

namespace GuedesTime.Domain.Intefaces
{
	public interface ISerieService : IDisposable
	{
		Task<PagedResult<Serie>> GetPagedByInstituicaoAsync(
			Guid instituicaoId,
			string? search,
			int page,
			int pageSize,
			bool ativo = true,
			Expression<Func<Serie, bool>>? filtroAdicional = null,
			Func<IQueryable<Serie>, IOrderedQueryable<Serie>>? ordenacao = null,
			params Expression<Func<Serie, object>>[]? includes
		);

		Task<Serie> ObterSeriePorNome(Guid instituicaoId, string nomeSerie);
		Task Atualizar(Serie serie);
		Task<Serie> ObterPorId(Guid id);
		Task ObterTodos();
		Task Remover(Guid id);
		Task AdicionarVariasAsync(IEnumerable<Serie> series);
		Task<List<string>> VerificarSeriesDuplicadasAsync(Guid instituicaoId, string? serieUnica, string? seriesMultiplas, EnumTipoEnsino tipoEnsino, Guid? idSerie = null);
	}
}