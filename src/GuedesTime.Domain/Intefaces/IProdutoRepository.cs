using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Intefaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        Task<Produto> ObterProdutoFornecedor(Guid id);
    }
}