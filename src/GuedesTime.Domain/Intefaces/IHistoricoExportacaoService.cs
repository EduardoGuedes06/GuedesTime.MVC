

using GuedesTime.Domain.Models;

namespace GuedesTime.Domain.Intefaces
{
    public interface IHistoricoExportacaoService : IDisposable
    {
        Task Adicionar(HistoricoExportacao historicoExportacao);
        Task Atualizar(HistoricoExportacao historicoExportacao);
        Task ObterPorId(HistoricoExportacao historicoExportacao);
        Task ObterTodos();
        Task Remover(Guid id);
    }
}