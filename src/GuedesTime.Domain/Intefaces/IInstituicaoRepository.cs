

using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Utils;

namespace GuedesTime.Domain.Intefaces
{
    public interface IInstituicaoRepository : IRepository<Instituicao>
    {
        Task<Instituicao?> ObterDadosInstituicoesPorId(Guid instituicaoId, bool? incluirInativos = false);
        Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId);
        Task<Instituicao?> ObterInstituicaoComEnderecoPorId(Guid id);
        Task<IEnumerable<Instituicao>> ObterInstituicoesDoUsuario(Guid usuarioId);
        Task<bool> VerificaUsuarioInstituicao(Guid usuarioId, Guid instituicaoId);
    }
}