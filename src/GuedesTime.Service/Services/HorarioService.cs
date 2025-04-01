using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class HorarioService : BaseService, IHorarioService
    {
        private readonly IHorarioRepository _horarioRepository;

        public HorarioService(IHorarioRepository HorarioRepository,
                              INotificador notificador) : base(notificador)
        {
            _horarioRepository = HorarioRepository;
        }

        public async Task ObterTodos()
        {
            await _horarioRepository.ObterTodos();
        }

        public async Task ObterPorId(Horario Horario)
        {
            await _horarioRepository.ObterPorId(Horario.Id);
        }

        public async Task Adicionar(Horario Horario)
        {
            await _horarioRepository.Adicionar(Horario);
        }

        public async Task Atualizar(Horario Horario)
        {
            await _horarioRepository.Atualizar(Horario);
        }

        public async Task Remover(Guid id)
        {
            await _horarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _horarioRepository?.Dispose();
        }
    }
}