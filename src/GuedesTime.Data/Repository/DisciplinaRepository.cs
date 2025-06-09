using System;
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
    public class DisciplinaRepository : Repository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(MeuDbContext context) : base(context) { }
        public async Task<Disciplina> ObterDisciplinaPorNome(Guid instituicaoId, string disciplinaNome)
        {
            return await Db.Disciplina
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.InstituicaoId == instituicaoId && i.Nome == disciplinaNome);
        }

    }
}