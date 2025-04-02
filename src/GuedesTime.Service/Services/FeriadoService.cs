using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class FeriadoService : BaseService, IFeriadoService
    {
        private readonly IFeriadoRepository _feriadoRepository;

        public FeriadoService(IFeriadoRepository FeriadoRepository,
                              INotificador notificador) : base(notificador)
        {
            _feriadoRepository = FeriadoRepository;
        }

        public async Task ObterTodos()
        {
            await _feriadoRepository.ObterTodos();
        }

        public async Task ObterPorId(Feriado Feriado)
        {
            await _feriadoRepository.ObterPorId(Feriado.Id);
        }

        public async Task Adicionar(Feriado Feriado)
        {
            await _feriadoRepository.Adicionar(Feriado);
        }

        public async Task Atualizar(Feriado Feriado)
        {
            await _feriadoRepository.Atualizar(Feriado);
        }

        public async Task Remover(Guid id)
        {
            await _feriadoRepository.Remover(id);
        }

        public void Dispose()
        {
            _feriadoRepository?.Dispose();
        }
    }
}