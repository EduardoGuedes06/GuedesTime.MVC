using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class TarefasService : BaseService, ITarefasService
    {
        private readonly ITarefasRepository _tarefaRepository;

        public TarefasService(ITarefasRepository TarefaRepository,
                              INotificador notificador) : base(notificador)
        {
            _tarefaRepository = TarefaRepository;
        }

        public async Task ObterTodos()
        {
            await _tarefaRepository.ObterTodos();
        }

        public async Task ObterPorId(Tarefas Tarefa)
        {
            await _tarefaRepository.ObterPorId(Tarefa.Id);
        }

        public async Task Adicionar(Tarefas Tarefa)
        {
            await _tarefaRepository.Adicionar(Tarefa);
        }

        public async Task Atualizar(Tarefas Tarefa)
        {
            await _tarefaRepository.Atualizar(Tarefa);
        }

        public async Task Remover(Guid id)
        {
            await _tarefaRepository.Remover(id);
        }

        public void Dispose()
        {
            _tarefaRepository?.Dispose();
        }
    }
}