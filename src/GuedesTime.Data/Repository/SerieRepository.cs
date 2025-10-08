using GuedesTime.Data.Context;
using GuedesTime.Data.Repository.Utils;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuedesTime.Data.Repository
{
	public class SerieRepository : Repository<Serie>, ISerieRepository
	{
		public SerieRepository(MeuDbContext context) : base(context) { }

		public async Task<Serie> ObterSeriePorNome(Guid instituicaoId, string serieNome)
		{
			return await Db.Serie
				.AsNoTracking()
				.FirstOrDefaultAsync(i => i.InstituicaoId == instituicaoId && i.Nome == serieNome);
		}

		public async Task<List<string>> ObterNomesSeriesDuplicadasAsync(
			Guid instituicaoId,
			List<string> nomesSeries,
			EnumTipoEnsino tipoEnsino,
			string? serieUnica = null,
			Guid? idSerie = null)
		{
			var query = Db.Serie
				.AsNoTracking()
				.Where(s =>
					s.InstituicaoId == instituicaoId &&
					s.TipoEnsino == tipoEnsino &&
					nomesSeries.Contains(s.Nome));

			if (!string.IsNullOrWhiteSpace(serieUnica) && idSerie.HasValue)
			{
				query = query.Where(s => !(s.Id == idSerie && s.Nome == serieUnica));
			}

			return await query
				.Select(s => s.Nome)
				.ToListAsync();
		}

        public async Task<Serie> ObterPorIdComDisciplinas(Guid id)
        {
            return await Db.Serie.AsNoTracking()
                .Include(s => s.Disciplinas)
                .ThenInclude(ds => ds.Disciplina)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public IQueryable<Serie> ObterQueryComDisciplinas()
        {
            return Db.Set<Serie>().AsNoTracking()
                .Include(s => s.Disciplinas)
                .ThenInclude(ds => ds.Disciplina);
        }

        public override async Task Remover(Guid id)
        {
            var disciplinasDaSerie = await Db.DisciplinaSerie
                .Where(ds => ds.SerieId == id)
                .ToListAsync();
            if (disciplinasDaSerie.Any())
            {
                Db.DisciplinaSerie.RemoveRange(disciplinasDaSerie);
            }
            var serie = await Db.Serie.FindAsync(id);
            if (serie != null)
            {
                Db.Serie.Remove(serie);
            }
        }

        public async Task SincronizarDisciplinasAsync(Guid serieId, IEnumerable<Guid> novasDisciplinaIds)
        {
            var associacoesAtuais = await Db.Set<DisciplinaSerie>()
                .Where(ds => ds.SerieId == serieId)
                .ToListAsync();

            var idsAtuais = associacoesAtuais.Select(ds => ds.DisciplinaId).ToList();

            var idsParaRemover = idsAtuais.Except(novasDisciplinaIds).ToList();
            if (idsParaRemover.Any())
            {
                var associacoesParaRemover = associacoesAtuais.Where(ds => idsParaRemover.Contains(ds.DisciplinaId));
                Db.Set<DisciplinaSerie>().RemoveRange(associacoesParaRemover);
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
                    Db.Set<DisciplinaSerie>().Add(novaAssociacao);
                }
            }
            await SaveChanges();
        }

    }
}