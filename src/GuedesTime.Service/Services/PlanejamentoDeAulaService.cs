using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class PlanejamentoDeAulaService : BaseService, IPlanejamentoDeAulaService
    {
        private readonly IPlanejamentoDeAulaRepository _planejamentoDeAulaRepository;

        public PlanejamentoDeAulaService(IPlanejamentoDeAulaRepository PlanejamentoDeAulaRepository,
                              INotificador notificador) : base(notificador)
        {
            _planejamentoDeAulaRepository = PlanejamentoDeAulaRepository;
        }

        public async Task ObterTodos()
        {
            await _planejamentoDeAulaRepository.ObterTodos();
        }

        public async Task ObterPorId(PlanejamentoDeAula PlanejamentoDeAula)
        {
            await _planejamentoDeAulaRepository.ObterPorId(PlanejamentoDeAula.Id);
        }

        public async Task Adicionar(PlanejamentoDeAula PlanejamentoDeAula)
        {
            await _planejamentoDeAulaRepository.Adicionar(PlanejamentoDeAula);
        }

        public async Task Atualizar(PlanejamentoDeAula PlanejamentoDeAula)
        {
            await _planejamentoDeAulaRepository.Atualizar(PlanejamentoDeAula);
        }

        public async Task Remover(Guid id)
        {
            await _planejamentoDeAulaRepository.Remover(id);
        }

        public void Dispose()
        {
            _planejamentoDeAulaRepository?.Dispose();
        }
    }
}