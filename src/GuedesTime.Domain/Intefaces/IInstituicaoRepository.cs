

using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IInstituicaoRepository : IRepository<Instituicao>
    {
        Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId);
        Task<Instituicao?> ObterInstituicaoComEnderecoPorId(Guid id);
    }
}