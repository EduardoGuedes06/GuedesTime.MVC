using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class AtividadesService : BaseService, IAtividadesService
    {
        private readonly IAtividadesRepository _atividadesRepository;

        public AtividadesService(IAtividadesRepository AtividadesRepository,
                              INotificador notificador) : base(notificador)
        {
            _atividadesRepository = AtividadesRepository;
        }

        public async Task ObterTodos()
        {
            await _atividadesRepository.ObterTodos();
        }

        public async Task ObterPorId(Atividades Atividades)
        {
            await _atividadesRepository.ObterPorId(Atividades.Id);
        }

        public async Task Adicionar(Atividades Atividades)
        {
            await _atividadesRepository.Adicionar(Atividades);
        }

        public async Task Atualizar(Atividades Atividades)
        {
            await _atividadesRepository.Atualizar(Atividades);
        }

        public async Task Remover(Guid id)
        {
            await _atividadesRepository.Remover(id);
        }

        public void Dispose()
        {
            _atividadesRepository?.Dispose();
        }
    }
}