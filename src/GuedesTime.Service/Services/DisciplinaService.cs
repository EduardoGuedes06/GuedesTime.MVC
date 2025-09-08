using GuedesTime.Data.Context;
using GuedesTime.Data.Repository;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;

namespace GuedesTime.Service.Services
{
    public class DisciplinaService : BaseService<Disciplina>, IDisciplinaService
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaService(IDisciplinaRepository disciplinaRepository,
                            INotificador notificador,
                            MeuDbContext context,
                            IPagedResultRepository<Disciplina> pagedRepository) : base(notificador, context, pagedRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task ObterTodos()
        {
            await _disciplinaRepository.ObterTodos();
        }

        public async Task<List<string>> VerificarDisciplinasDuplicadasAsync(
            Guid instituicaoId,
            string? disciplinaUnica,
            string? disciplinasMultiplas,
            Guid? idDisciplina = null)
        {
            var nomes = new List<string>();

            if (!string.IsNullOrWhiteSpace(disciplinaUnica))
                nomes.Add(disciplinaUnica.Trim());

            if (!string.IsNullOrWhiteSpace(disciplinasMultiplas))
            {
                var nomesMultiplos = disciplinasMultiplas
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToList();

                nomes.AddRange(nomesMultiplos);
            }

            if (!nomes.Any())
                return new List<string>();

            return await _disciplinaRepository.ObterNomesDisciplinasDuplicadasAsync(
                instituicaoId,
                nomes,
                idDisciplina
            );
        }

        public async Task<Disciplina> ObterDisciplinaPorNome(Guid instituicaoId, string nomeDisciplina)
        {
            return await _disciplinaRepository.ObterDisciplinaPorNome(instituicaoId, nomeDisciplina);
        }
        public async Task<(bool Existe, List<string> NomesExistentes)> VerificarDisciplinasExistentesPorNomes(Guid instituicaoId, List<string> nomes)
        {
            return await _disciplinaRepository.VerificarDisciplinasExistentesPorNomes(instituicaoId, nomes);
        }
        public async Task AdicionarVariasAsync(IEnumerable<Disciplina> disciplinas)
        {

            foreach (var disciplina in disciplinas)
            {
                if (disciplina == null) continue;
                await _disciplinaRepository.Adicionar(disciplina);
            }
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