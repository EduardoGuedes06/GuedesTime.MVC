using GuedesTime.Data.Context;
using GuedesTime.Data.Repository;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class SerieService : BaseService<Serie>, ISerieService
    {
        private readonly ISerieRepository _serieRepository;

        public SerieService(ISerieRepository serieRepository,
                              INotificador notificador,
							  MeuDbContext context,
							  IPagedResultRepository<Serie> pagedRepository) : base(notificador, context, pagedRepository)
		{
            _serieRepository = serieRepository;
        }

        public async Task ObterTodos()
        {
            await _serieRepository.ObterTodos();
        }

		public async Task<Serie> ObterPorId(Guid id) => await _serieRepository.ObterPorId(id);

		public async Task<Serie> ObterSeriePorNome(Guid instituicaoId, string nomeSerie)
		{
			return await _serieRepository.ObterSeriePorNome(instituicaoId, nomeSerie);
		}

		public async Task Adicionar(Serie Serie)
        {
            await _serieRepository.Adicionar(Serie);
        }

        public async Task Atualizar(Serie Serie)
        {
            await _serieRepository.Atualizar(Serie);
        }

        public async Task Remover(Guid id)
        {
            await _serieRepository.Remover(id);
        }

        public void Dispose()
        {
            _serieRepository?.Dispose();
        }
    }
}