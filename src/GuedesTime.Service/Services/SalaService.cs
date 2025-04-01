using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class SalaService : BaseService, ISalaService
    {
        private readonly ISalaRepository _salaRepository;

        public SalaService(ISalaRepository SalaRepository,
                              INotificador notificador) : base(notificador)
        {
            _salaRepository = SalaRepository;
        }

        public async Task ObterTodos()
        {
            await _salaRepository.ObterTodos();
        }

        public async Task ObterPorId(Sala Sala)
        {
            await _salaRepository.ObterPorId(Sala.Id);
        }

        public async Task Adicionar(Sala Sala)
        {
            await _salaRepository.Adicionar(Sala);
        }

        public async Task Atualizar(Sala Sala)
        {
            await _salaRepository.Atualizar(Sala);
        }

        public async Task Remover(Guid id)
        {
            await _salaRepository.Remover(id);
        }

        public void Dispose()
        {
            _salaRepository?.Dispose();
        }
    }
}