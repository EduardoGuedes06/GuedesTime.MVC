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
    public class DisciplinaRepository : Repository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(MeuDbContext context) : base(context) { }
        public async Task<Disciplina> ObterDisciplinaPorNome(Guid instituicaoId, string disciplinaNome)
        {
            return await Db.Disciplina
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.InstituicaoId == instituicaoId && i.Nome == disciplinaNome);
        }

        public async Task<List<string>> ObterNomesDisciplinasDuplicadasAsync(
            Guid instituicaoId,
            List<string> nomesDisciplinas,
            Guid? idDisciplina = null)
        {
            var query = Db.Disciplina
                .AsNoTracking()
                .Where(s =>
                    s.InstituicaoId == instituicaoId &&
                    nomesDisciplinas.Contains(s.Nome));

            if (idDisciplina.HasValue)
            {
                query = query.Where(s => s.Id != idDisciplina.Value || !nomesDisciplinas.Contains(s.Nome));
            }

            return await query
                .Select(s => s.Nome)
                .ToListAsync();
        }
        public async Task<(bool Existe, List<string> NomesExistentes)> VerificarDisciplinasExistentesPorNomes(Guid instituicaoId, List<string> nomes)
        {
            var nomesExistentes = await Db.Disciplina
                .AsNoTracking()
                .Where(d => d.InstituicaoId == instituicaoId && nomes.Contains(d.Nome))
                .Select(d => d.Nome)
                .ToListAsync();

            bool existe = nomesExistentes.Any();

            return (existe, nomesExistentes);
        }

    }
}