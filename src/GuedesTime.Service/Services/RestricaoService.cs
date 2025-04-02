using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class RestricaoService : BaseService, IRestricaoService
    {
        private readonly IRestricaoRepository _restricaoRepository;

        public RestricaoService(IRestricaoRepository RestricaoRepository,
                              INotificador notificador) : base(notificador)
        {
            _restricaoRepository = RestricaoRepository;
        }

        public async Task ObterTodos()
        {
            await _restricaoRepository.ObterTodos();
        }

        public async Task ObterPorId(Restricao Restricao)
        {
            await _restricaoRepository.ObterPorId(Restricao.Id);
        }

        public async Task Adicionar(Restricao Restricao)
        {
            await _restricaoRepository.Adicionar(Restricao);
        }

        public async Task Atualizar(Restricao Restricao)
        {
            await _restricaoRepository.Atualizar(Restricao);
        }

        public async Task Remover(Guid id)
        {
            await _restricaoRepository.Remover(id);
        }

        public void Dispose()
        {
            _restricaoRepository?.Dispose();
        }
    }
}