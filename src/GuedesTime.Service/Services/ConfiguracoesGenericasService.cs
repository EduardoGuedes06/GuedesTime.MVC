using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class ConfiguracoesGenericasService : BaseService, IConfiguracoesGenericasService
    {
        private readonly IConfiguracoesGenericasRepository _configuracoesGenericasRepository;

        public ConfiguracoesGenericasService(IConfiguracoesGenericasRepository ConfiguracoesGenericasRepository,
                              INotificador notificador) : base(notificador)
        {
            _configuracoesGenericasRepository = ConfiguracoesGenericasRepository;
        }

        public async Task ObterTodos()
        {
            await _configuracoesGenericasRepository.ObterTodos();
        }

        public async Task ObterPorId(ConfiguracoesGenericas ConfiguracoesGenericas)
        {
            await _configuracoesGenericasRepository.ObterPorId(ConfiguracoesGenericas.Id);
        }

        public async Task Adicionar(ConfiguracoesGenericas ConfiguracoesGenericas)
        {
            await _configuracoesGenericasRepository.Adicionar(ConfiguracoesGenericas);
        }

        public async Task Atualizar(ConfiguracoesGenericas ConfiguracoesGenericas)
        {
            await _configuracoesGenericasRepository.Atualizar(ConfiguracoesGenericas);
        }

        public async Task Remover(Guid id)
        {
            await _configuracoesGenericasRepository.Remover(id);
        }

        public void Dispose()
        {
            _configuracoesGenericasRepository?.Dispose();
        }
    }
}