

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IInstituicaoRepository : IRepository<Instituicao>
    {
        Task<PagedResult<Instituicao>> GetPaged(Guid usuarioId, string? search, int pageSize, int? page = null, bool ativo = true);
        Task<Instituicao?> ObterDadosInstituicoesPorId(Guid instituicaoId);
        Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId);
        Task<Instituicao?> ObterInstituicaoComEnderecoPorId(Guid id);
        Task<IEnumerable<Instituicao>> ObterInstituicoesDoUsuario(Guid usuarioId);
    }
}