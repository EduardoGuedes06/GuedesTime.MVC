

using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;

namespace GuedesTime.Domain.Intefaces
{
	public interface ISerieRepository : IRepository<Serie>
	{
		Task<Serie> ObterSeriePorNome(Guid instituicaoId, string serieNome);
	}
}