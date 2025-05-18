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
                .Include(i => i.Professores)
                .Include(i => i.Turmas)
                .Include(i => i.Series)
                .Include(i => i.Disciplinas)
                .Include(i => i.Salas)
                .Include(i => i.Horarios)
                .Include(i => i.Feriados)
                .Include(i => i.Endereco)
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }
        public async Task<Instituicao?> ObterDadosInstituicoesPorId(Guid instituicaoId)
        {
            return await Db.Instituicao.AsNoTracking()
                .Include(i => i.Professores)
                .Include(i => i.Turmas)
                .Include(i => i.Series)
                .Include(i => i.Disciplinas)
                .Include(i => i.Salas)
                .Include(i => i.Horarios)
                .Include(i => i.Feriados)
                .Include(i => i.Endereco)
                .OrderBy(i => i.Nome)
                .FirstOrDefaultAsync(i => i.Id == instituicaoId);
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

        public async Task<PagedResult<Instituicao>> GetPaged(Guid usuarioId, string? search, int pageSize, int? page = null, bool ativo = true)
        {
            IQueryable<Instituicao> query = Db.Instituicao
                .AsNoTracking()
                .Where(c => c.Ativo == ativo && c.UsuarioId == usuarioId);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c =>
                    c.Nome.Contains(search) ||
                    c.Cnpj.Contains(search) ||
                    c.CodigoCie.Contains(search));
            }

            return await _pagedResultRepository.GetPagedResultAsync(query, pageSize, page);
        }

        public async Task<bool> VerificaUsuarioInstituicao(Guid usuarioId, Guid instituicaoId)
        {
            return await Db.Instituicao.AsNoTracking()
                .AnyAsync(i => i.UsuarioId == usuarioId && i.Id == instituicaoId);
        }

    }
}