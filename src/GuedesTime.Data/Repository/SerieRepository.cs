﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuedesTime.Data.Context;
using GuedesTime.Data.Repository.Utils;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;

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
	}
}