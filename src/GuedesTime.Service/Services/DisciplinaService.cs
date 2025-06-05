using GuedesTime.Data.Context;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{

    public class DisciplinaService : BaseService<Disciplina>, IDisciplinaService
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaService(
            IDisciplinaRepository disciplinaRepository,
            INotificador notificador,
            MeuDbContext context,
            IPagedResultRepository<Disciplina> pagedRepository)
            : base(notificador, context, pagedRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task ObterTodos()
        {
            await _disciplinaRepository.ObterTodos();
        }

        public async Task<bool> ObterDisciplinaPorNome(Guid instituicaoId, string nomeDisciplina)
        {
            return await _disciplinaRepository.ObterDisciplinaPorNome(instituicaoId, nomeDisciplina);
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

        public async Task<Disciplina> ObterPorId(Guid DisciplinaId)
        {
            return await _disciplinaRepository.ObterPorId(DisciplinaId);
        }
    }
}