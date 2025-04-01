using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class TurmaService : BaseService, ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmaService(ITurmaRepository TurmaRepository,
                              INotificador notificador) : base(notificador)
        {
            _turmaRepository = TurmaRepository;
        }

        public async Task ObterTodos()
        {
            await _turmaRepository.ObterTodos();
        }

        public async Task ObterPorId(Turma turma)
        {
            await _turmaRepository.ObterPorId(turma.Id);
        }

        public async Task Adicionar(Turma turma)
        {
            await _turmaRepository.Adicionar(turma);
        }

        public async Task Atualizar(Turma turma)
        {
            await _turmaRepository.Atualizar(turma);
        }

        public async Task Remover(Guid id)
        {
            await _turmaRepository.Remover(id);
        }

        public void Dispose()
        {
            _turmaRepository?.Dispose();
        }
    }
}