using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class ProfessorService : BaseService, IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository ProfessorRepository,
                              INotificador notificador) : base(notificador)
        {
            _professorRepository = ProfessorRepository;
        }

        public async Task ObterTodos()
        {
            await _professorRepository.ObterTodos();
        }

        public async Task ObterPorId(Professor Professor)
        {
            await _professorRepository.ObterPorId(Professor.Id);
        }

        public async Task Adicionar(Professor Professor)
        {
            await _professorRepository.Adicionar(Professor);
        }

        public async Task Atualizar(Professor Professor)
        {
            await _professorRepository.Atualizar(Professor);
        }

        public async Task Remover(Guid id)
        {
            await _professorRepository.Remover(id);
        }

        public void Dispose()
        {
            _professorRepository?.Dispose();
        }
    }
}