

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IInstituicaoService : IDisposable
    {
        Task Adicionar(Instituicao instituicao);
        Task Atualizar(Instituicao instituicao);
        Task<string> ObterAvatarAleatorioAsync();
        Task<Instituicao> ObterDadosInstituicoesPorId(Guid instituicaoId);
        Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId);
        Task<Instituicao> ObterInstituicaoComEnderecoPorId(Guid instituicaoId);
        Task<PagedResult<Instituicao>> ObterInstituiceosPaginada(Guid usuarioId, string? search, int page, int pageSize, bool ativo);
        Task<Instituicao> ObterPorId(Guid instituicaoId);
        Task Remover(Guid id);
        Task<bool> VerificaUsuarioInstituicao(Guid usuarioId, Guid instituicaoId);
    }
}