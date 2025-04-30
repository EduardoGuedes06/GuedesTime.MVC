using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class SerieService : BaseService, ISerieService
    {
        private readonly ISerieService _serieRepository;

        public SerieService(ISerieService SerieRepository,
                              INotificador notificador) : base(notificador)
        {
            _serieRepository = SerieRepository;
        }

        public async Task ObterTodos()
        {
            await _serieRepository.ObterTodos();
        }

        public async Task ObterPorId(Guid id)
        {
            await _serieRepository.ObterPorId(id);
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