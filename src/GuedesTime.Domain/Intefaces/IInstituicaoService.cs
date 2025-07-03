﻿

using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Utils;
using System.Linq.Expressions;

namespace GuedesTime.Domain.Intefaces
{
    public interface IInstituicaoService : IDisposable
    {
		Task<PagedResult<Instituicao>> GetPagedByInstituicaoAsync(
			Guid instituicaoId,
			string? search,
			int page,
			int pageSize,
			bool ativo = true,
			Expression<Func<Instituicao, bool>>? filtroAdicional = null,
			Func<IQueryable<Instituicao>, IOrderedQueryable<Instituicao>>? ordenacao = null,
			params Expression<Func<Instituicao, object>>[]? includes
		);
		Task Adicionar(Instituicao instituicao);
        Task Atualizar(Instituicao instituicao);
        Task<string> ObterAvatarAleatorioAsync();
        Task<Dictionary<Guid, DadosAgregadosInstituicao>> ObterCalculoGeralDosDadosDaInstituicao(List<Guid> instituicaoIds);
        Task<Instituicao> ObterDadosInstituicoesPorId(Guid instituicaoId);
        Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId);
        Task<Instituicao> ObterInstituicaoComEnderecoPorId(Guid instituicaoId);
        Task<Instituicao> ObterPorId(Guid instituicaoId);
        Task Remover(Guid id);
        Task<bool> VerificaUsuarioInstituicao(Guid usuarioId, Guid instituicaoId);
    }
}