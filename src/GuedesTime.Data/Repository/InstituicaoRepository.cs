using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GuedesTime.Data.Context;
using GuedesTime.Data.Repository.Utils;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Utils;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.Data.Repository
{
    public class InstituicaoRepository : Repository<Instituicao>, IInstituicaoRepository
    {
        private readonly IPagedResultRepository<Instituicao> _pagedResultRepository;

        public InstituicaoRepository(MeuDbContext context, IPagedResultRepository<Instituicao> pagedResultRepository)
            : base(context)
        {
            _pagedResultRepository = pagedResultRepository;
        }

        public async Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId)
        {
            return await FiltrarComRelacionamentos(i => i.UsuarioId == usuarioId)
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }

        public async Task<Instituicao?> ObterDadosInstituicoesPorId(Guid instituicaoId, bool? incluirInativos = false)
        {
            var instituicao = await FiltrarComRelacionamentos(i => i.Id == instituicaoId).FirstOrDefaultAsync();

            if (instituicao == null) return null;

            return FiltrarInativos(instituicao, incluirInativos);
        }

        public async Task<Instituicao?> ObterInstituicaoComEnderecoPorId(Guid id)
        {
            return await Db.Instituicao.AsNoTracking()
                .Include(i => i.Endereco)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Instituicao>> ObterInstituicoesDoUsuario(Guid usuarioId)
        {
            return await Db.Instituicao.AsNoTracking()
                .Where(i => i.UsuarioId == usuarioId)
                .Include(i => i.Endereco)
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }

        public async Task<bool> VerificaUsuarioInstituicao(Guid usuarioId, Guid instituicaoId)
        {
            return await Db.Instituicao.AsNoTracking()
                .AnyAsync(i => i.UsuarioId == usuarioId && i.Id == instituicaoId);
        }

        #region Utils

        private IQueryable<Instituicao> FiltrarComRelacionamentos(Expression<Func<Instituicao, bool>> predicate)
        {
            return IncluirRelacionamentos(Db.Instituicao.AsNoTracking().Where(predicate));
        }

        private IQueryable<Instituicao> IncluirRelacionamentos(IQueryable<Instituicao> query)
        {
            return query
                .Include(i => i.Professores)
                .Include(i => i.Turmas)
                .Include(i => i.Series)
                .Include(i => i.Disciplinas)
                .Include(i => i.Salas)
                .Include(i => i.Horarios)
                .Include(i => i.Feriados)
                .Include(i => i.Endereco);
        }

        private Instituicao FiltrarInativos(Instituicao instituicao, bool? incluirInativos)
        {
            if (incluirInativos is not true)
            {
                instituicao.Professores = instituicao.Professores?.Where(p => p.Ativo ?? false).ToList();
                instituicao.Disciplinas = instituicao.Disciplinas?.Where(d => d.Ativo ?? false).ToList();
            }
            return instituicao;
        }

        #endregion
    }
}
