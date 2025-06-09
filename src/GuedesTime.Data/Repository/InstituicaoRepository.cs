using System.Linq.Expressions;
using GuedesTime.Data.Context;
using GuedesTime.Data.Repository.Utils;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
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
            return await Db.Instituicao.AsNoTracking()
                .Where(i => i.UsuarioId == usuarioId)
                .Include(i => i.Endereco)
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }

        public async Task<Instituicao?> ObterDadosInstituicoesPorId(Guid instituicaoId, bool? incluirInativos = false)
        {
            return await BuscarInstituicaoCompletaAsync(i => i.Id == instituicaoId, 5, incluirInativos);
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

        private async Task<Instituicao?> BuscarInstituicaoCompletaAsync(Expression<Func<Instituicao, bool>> filtro, int top = 5, bool? incluirInativos = false)
        {
            var instituicao = await Db.Instituicao
                .AsNoTracking()
                .FirstOrDefaultAsync(filtro);

            if (instituicao == null)
                return null;

            await CarregarTopRelacionamentosAsync(instituicao, top);
            return FiltrarInativos(instituicao, incluirInativos);
        }

        private async Task CarregarTopRelacionamentosAsync(Instituicao instituicao, int top)
        {
            var id = instituicao.Id;

            instituicao.Professores = await Db.Professor
                .Where(p => p.InstituicaoId == id)
                .OrderBy(p => p.Nome)
                .Take(top).ToListAsync();

            instituicao.Turmas = await Db.Turma
                .Where(t => t.InstituicaoId == id)
                .OrderBy(t => t.Nome)
                .Take(top).ToListAsync();

            instituicao.Series = await Db.Serie
                .Where(s => s.InstituicaoId == id)
                .OrderBy(s => s.Nome)
                .Take(top).ToListAsync();

            instituicao.Disciplinas = await Db.Disciplina
                .Where(d => d.InstituicaoId == id)
                .OrderBy(d => d.Nome)
                .Take(top).ToListAsync();

            instituicao.Salas = await Db.Sala
                .Where(s => s.InstituicaoId == id)
                .OrderBy(s => s.Nome)
                .Take(top).ToListAsync();

            instituicao.Horarios = await Db.Horario
                .Where(h => h.InstituicaoId == id)
                .OrderBy(h => h.Inicio)
                .Take(top).ToListAsync();

            instituicao.Feriados = await Db.Feriado
                .Where(f => f.InstituicaoId == id)
                .OrderBy(f => f.Data)
                .Take(top).ToListAsync();

            instituicao.Endereco = await Db.Endereco
                .FirstOrDefaultAsync(f => f.InstituicaoId == id);
        }

        private Instituicao FiltrarInativos(Instituicao instituicao, bool? incluirInativos)
        {
            if (incluirInativos is not true)
            {
                instituicao.Professores = instituicao.Professores?
                    .Where(p => p.Ativo ?? false).ToList();

                instituicao.Disciplinas = instituicao.Disciplinas?
                    .Where(d => d.Ativo ?? false).ToList();
            }

            return instituicao;
        }

        #endregion
    }
}
