using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class HistoricoExportacaoService : BaseService, IHistoricoExportacaoService
    {
        private readonly IHistoricoExportacaoRepository _historicoExportacaoRepository;

        public HistoricoExportacaoService(IHistoricoExportacaoRepository HistoricoExportacaoRepository,
                              INotificador notificador) : base(notificador)
        {
            _historicoExportacaoRepository = HistoricoExportacaoRepository;
        }

        public async Task ObterTodos()
        {
            await _historicoExportacaoRepository.ObterTodos();
        }

        public async Task ObterPorId(HistoricoExportacao HistoricoExportacao)
        {
            await _historicoExportacaoRepository.ObterPorId(HistoricoExportacao.Id);
        }

        public async Task Adicionar(HistoricoExportacao HistoricoExportacao)
        {
            await _historicoExportacaoRepository.Adicionar(HistoricoExportacao);
        }

        public async Task Atualizar(HistoricoExportacao HistoricoExportacao)
        {
            await _historicoExportacaoRepository.Atualizar(HistoricoExportacao);
        }

        public async Task Remover(Guid id)
        {
            await _historicoExportacaoRepository.Remover(id);
        }

        public void Dispose()
        {
            _historicoExportacaoRepository?.Dispose();
        }
    }
}