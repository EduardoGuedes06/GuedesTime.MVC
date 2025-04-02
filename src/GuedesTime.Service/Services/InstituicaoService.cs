using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class InstituicaoService : BaseService, IInstituicaoService
    {
        private readonly IInstituicaoRepository _instituicaoRepository;

        public InstituicaoService(IInstituicaoRepository InstituicaoRepository,
                              INotificador notificador) : base(notificador)
        {
            _instituicaoRepository = InstituicaoRepository;
        }

        public async Task ObterTodos()
        {
            await _instituicaoRepository.ObterTodos();
        }

        public async Task<Instituicao> ObterPorId(Guid instituicaoId)
        {
            return await _instituicaoRepository.ObterPorId(instituicaoId);
        }

        public async Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId)
        {
            return await _instituicaoRepository.ObterDadosInstituicoesUsuario(usuarioId);
        }


        public async Task Adicionar(Instituicao Instituicao)
        {
            await _instituicaoRepository.Adicionar(Instituicao);
        }

        public async Task Atualizar(Instituicao Instituicao)
        {
            await _instituicaoRepository.Atualizar(Instituicao);
        }

        public async Task Remover(Guid id)
        {
            await _instituicaoRepository.Remover(id);
        }

        public void Dispose()
        {
            _instituicaoRepository?.Dispose();
        }
    }
}