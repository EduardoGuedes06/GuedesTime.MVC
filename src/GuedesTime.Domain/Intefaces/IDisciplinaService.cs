﻿

using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Utils;
using System.Linq.Expressions;

namespace GuedesTime.Domain.Intefaces
{
    public interface IDisciplinaService : IDisposable
    {
		Task<PagedResult<Disciplina>> GetPagedByInstituicaoAsync(
			Guid instituicaoId,
			string? search,
			int page,
			int pageSize,
			bool ativo = true,
			Expression<Func<Disciplina, bool>>? filtroAdicional = null,
			Func<IQueryable<Disciplina>, IOrderedQueryable<Disciplina>>? ordenacao = null,
			params Expression<Func<Disciplina, object>>[]? includes
		);
		Task Adicionar(Disciplina disciplina);
        Task AdicionarDisciplinas(Guid InstituicaoId, List<string> listaDeDisciplinas);
		Task Atualizar(Disciplina disciplina);
        Task<Disciplina> ObterPorId(Guid DisciplinaId);
        Task ObterTodos();
        Task Remover(Guid id);
        Task<Disciplina> ObterDisciplinaPorNome(Guid instituicaoId, string nomeDisciplina);
		Task<(bool Existe, List<string> NomesExistentes)> VerificarDisciplinasExistentesPorNomes(Guid instituicaoId, List<string> nomes);
	}
}