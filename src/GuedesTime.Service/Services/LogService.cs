using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class LogService : BaseService, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository LogRepository,
                              INotificador notificador) : base(notificador)
        {
            _logRepository = LogRepository;
        }

        public async Task ObterTodos()
        {
            await _logRepository.ObterTodos();
        }

        public async Task ObterPorId(Log Log)
        {
            await _logRepository.ObterPorId(Log.Id);
        }

        public async Task Adicionar(Log Log)
        {
            await _logRepository.Adicionar(Log);
        }

        public async Task Atualizar(Log Log)
        {
            await _logRepository.Atualizar(Log);
        }

        public async Task Remover(Guid id)
        {
            await _logRepository.Remover(id);
        }

        public void Dispose()
        {
            _logRepository?.Dispose();
        }
    }
}