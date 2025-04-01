

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IConfiguracoesGenericasService : IDisposable
    {
        Task Adicionar(ConfiguracoesGenericas configuracoesGenericas);
        Task Atualizar(ConfiguracoesGenericas configuracoesGenericas);
        Task ObterPorId(ConfiguracoesGenericas configuracoesGenericas);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}