using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuedesTime.Data.Context;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.Data.Repository
{
    public class InstituicaoRepository : Repository<Instituicao>, IInstituicaoRepository
    {
        public InstituicaoRepository(MeuDbContext context) : base(context) { }
        public async Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId)
        {
            return await Db.Instituicao.AsNoTracking()
                .Where(i => i.UsuarioId == usuarioId)
                .Include(i => i.Professores)
                .Include(i => i.Turmas)
                .Include(i => i.Disciplinas)
                .Include(i => i.Salas)
                .Include(i => i.Horarios)
                .Include(i => i.Feriados)
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }
    }
}