using GuedesTime.Data.Context;
using GuedesTime.Data.Repository;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;
using GuedesTime.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Serie> ObterPorIdComDisciplinasAsync(Guid id)
        {
            return await _serieRepository.ObterPorIdComDisciplinas(id);
        }


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

        public async Task<PagedResult<Serie>> ObterSeriesPaginadoComDisciplinas(
            Guid instituicaoId,
            string? search,
            int page,
            int pageSize,
            bool ativo,
            EnumTipoEnsino? tipoEnsino = null)
        {
            var query = _serieRepository.ObterQueryComDisciplinas()
                .Where(s => s.InstituicaoId == instituicaoId && s.Ativo == ativo);

            if (tipoEnsino.HasValue)
            {
                query = query.Where(s => s.TipoEnsino == tipoEnsino.Value);
            }

            query = ApplySearch(query, search);
            query = query.OrderBy(s => s.TipoEnsino).ThenBy(s => s.Nome);

            return await PaginarResultadoAsync(query, pageSize, page);
        }
        public async Task<Serie> ObterSeriePorNome(Guid instituicaoId, string nomeSerie)
		{
			return await _serieRepository.ObterSeriePorNome(instituicaoId, nomeSerie);
		}

        public async Task SincronizarDisciplinasAsync(Guid serieId, IEnumerable<Guid> novasDisciplinaIds)
        {
            var associacoesAtuais = await _context.DisciplinaSerie
                .Where(ds => ds.SerieId == serieId)
                .ToListAsync();

            var idsAtuais = associacoesAtuais.Select(ds => ds.DisciplinaId).ToList();

            var idsParaRemover = idsAtuais.Except(novasDisciplinaIds).ToList();
            if (idsParaRemover.Any())
            {
                var associacoesParaRemover = associacoesAtuais.Where(ds => idsParaRemover.Contains(ds.DisciplinaId));
                _context.DisciplinaSerie.RemoveRange(associacoesParaRemover);
            }

            var idsParaAdicionar = novasDisciplinaIds.Except(idsAtuais).ToList();
            if (idsParaAdicionar.Any())
            {
                foreach (var disciplinaId in idsParaAdicionar)
                {
                    var novaAssociacao = new DisciplinaSerie
                    {
                        SerieId = serieId,
                        DisciplinaId = disciplinaId,
                        CargaHoraria = TimeSpan.FromHours(1)
                    };
                    await _context.DisciplinaSerie.AddAsync(novaAssociacao);
                }
            }
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