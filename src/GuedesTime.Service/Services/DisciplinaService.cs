using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class DisciplinaService : BaseService, IDisciplinaService
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaService(IDisciplinaRepository DisciplinaRepository,
                              INotificador notificador) : base(notificador)
        {
            _disciplinaRepository = DisciplinaRepository;
        }

        public async Task ObterTodos()
        {
            await _disciplinaRepository.ObterTodos();
        }

        public async Task ObterPorId(Disciplina Disciplina)
        {
            await _disciplinaRepository.ObterPorId(Disciplina.Id);
        }

        public async Task Adicionar(Disciplina Disciplina)
        {
            await _disciplinaRepository.Adicionar(Disciplina);
        }

        public async Task Atualizar(Disciplina Disciplina)
        {
            await _disciplinaRepository.Atualizar(Disciplina);
        }

        public async Task Remover(Guid id)
        {
            await _disciplinaRepository.Remover(id);
        }

        public void Dispose()
        {
            _disciplinaRepository?.Dispose();
        }
    }
}