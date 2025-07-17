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


	}
}