using GuedesTime.Data.Context;
using GuedesTime.Data.Repository;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;

namespace GuedesTime.Service.Services
{
    public class SerieService : BaseService<Serie>, ISerieService
    {
        private readonly ISerieRepository _serieRepository;

        public SerieService(ISerieRepository serieRepository,
                              INotificador notificador,
							  MeuDbContext context,
							  IPagedResultRepository<Serie> pagedRepository) : base(notificador, context, pagedRepository)
		{
            _serieRepository = serieRepository;
        }

        public async Task ObterTodos()
        {
            await _serieRepository.ObterTodos();
        }

		public async Task<Serie> ObterPorId(Guid id) => await _serieRepository.ObterPorId(id);

		public async Task<List<string>> VerificarSeriesDuplicadasAsync(
		Guid instituicaoId,
		string? serieUnica,
		string? seriesMultiplas,
		EnumTipoEnsino tipoEnsino,
		Guid? idSerie = null)
		{
			var nomes = new List<string>();

			if (!string.IsNullOrWhiteSpace(serieUnica))
				nomes.Add(serieUnica.Trim());

			if (!string.IsNullOrWhiteSpace(seriesMultiplas))
			{
				var nomesMultiplos = seriesMultiplas
					.Split(',', StringSplitOptions.RemoveEmptyEntries)
					.Select(s => s.Trim())
					.Where(s => !string.IsNullOrWhiteSpace(s))
					.ToList();

				nomes.AddRange(nomesMultiplos);
			}

			if (!nomes.Any())
				return new List<string>();

			return await _serieRepository.ObterNomesSeriesDuplicadasAsync(
				instituicaoId,
				nomes,
				tipoEnsino,
				serieUnica,
				idSerie
			);
		}

		public async Task<Serie> ObterSeriePorNome(Guid instituicaoId, string nomeSerie)
		{
			return await _serieRepository.ObterSeriePorNome(instituicaoId, nomeSerie);
		}


		public async Task AdicionarVariasAsync(IEnumerable<Serie> series)
		{

			foreach (var serie in series)
			{
				if (serie == null) continue;
				await _serieRepository.Adicionar(serie);
			}
		}

		public async Task Atualizar(Serie Serie)
        {
            await _serieRepository.Atualizar(Serie);
        }

        public async Task Remover(Guid id)
        {
            await _serieRepository.Remover(id);
        }

        public void Dispose()
        {
            _serieRepository?.Dispose();
        }
    }
}