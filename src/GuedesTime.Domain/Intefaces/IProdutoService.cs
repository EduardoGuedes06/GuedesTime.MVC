

using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}