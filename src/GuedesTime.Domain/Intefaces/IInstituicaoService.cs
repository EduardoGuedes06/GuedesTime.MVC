

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IInstituicaoService : IDisposable
    {
        Task Adicionar(Instituicao instituicao);
        Task Atualizar(Instituicao instituicao);
        Task<string> ObterAvatarAleatorioAsync();
        Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId);
        Task<Instituicao> ObterInstituicaoComEnderecoPorId(Guid instituicaoId);
        Task<Instituicao> ObterPorId(Guid instituicaoId);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}