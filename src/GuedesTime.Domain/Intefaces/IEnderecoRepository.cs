using GuedesTime.Domain.Models;
using GuedesTime.Service.Models;
using System;
using System.Threading.Tasks;

namespace GuedesTime.Domain.Intefaces
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
    }
}