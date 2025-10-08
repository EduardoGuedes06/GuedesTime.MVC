

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
            bool ativo,
            Expression<Func<Disciplina, bool>>? filtroAdicional,
            Func<IQueryable<Disciplina>, IOrderedQueryable<Disciplina>>? ordenacao,
            IQueryable<Disciplina>? sourceQuery,
            params Expression<Func<Disciplina, object>>[]? includes
        );
        Task Atualizar(Disciplina disciplina);
        Task<Disciplina> ObterPorId(Guid DisciplinaId);
        Task ObterTodos();
        Task Remover(Guid id);
        Task<Disciplina> ObterDisciplinaPorNome(Guid instituicaoId, string nomeDisciplina);
		Task<(bool Existe, List<string> NomesExistentes)> VerificarDisciplinasExistentesPorNomes(Guid instituicaoId, List<string> nomes);
        Task<List<string>> VerificarDisciplinasDuplicadasAsync(Guid instituicaoId, string? nome, string? nomes, Guid? idDisciplina = null);
        Task AdicionarVariasAsync(IEnumerable<Disciplina> disciplinas);
        Task<IEnumerable<Disciplina>> BuscarPorNomeAsync(Guid instituicaoId, string nomeQuery);
    }
}