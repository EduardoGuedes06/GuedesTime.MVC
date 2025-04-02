using GuedesTime.Data.Repository;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;

namespace GuedesTime.Service.Services
{
    public class InstituicaoService : BaseService, IInstituicaoService
    {
        private readonly IInstituicaoRepository _instituicaoRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public InstituicaoService(IInstituicaoRepository InstituicaoRepository,
                                IEnderecoRepository enderecoRepository,
                              INotificador notificador) : base(notificador)
        {
            _instituicaoRepository = InstituicaoRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task ObterTodos()
        {
            await _instituicaoRepository.ObterTodos();
        }

        public async Task<Instituicao> ObterPorId(Guid instituicaoId)
        {
            return await _instituicaoRepository.ObterPorId(instituicaoId);
        }

        public async Task<Instituicao> ObterInstituicaoComEnderecoPorId(Guid instituicaoId)
        {
            return await _instituicaoRepository.ObterInstituicaoComEnderecoPorId(instituicaoId);
        }

        public async Task<IEnumerable<Instituicao>> ObterDadosInstituicoesUsuario(Guid usuarioId)
        {
            return await _instituicaoRepository.ObterDadosInstituicoesUsuario(usuarioId);
        }


        public async Task Adicionar(Instituicao Instituicao)
        {
            await _instituicaoRepository.Adicionar(Instituicao);
        }

        public async Task Atualizar(Instituicao instituicao)
        {
            var instituicaoExistente = await _instituicaoRepository.ObterInstituicaoComEnderecoPorId(instituicao.Id);

            if (instituicaoExistente == null)
                throw new Exception("Instituição não encontrada.");

            if (instituicao.Endereco != null)
            {
                instituicao.Endereco.InstituicaoId = instituicao.Id;
                instituicao.Endereco.Id = instituicaoExistente.Endereco.Id;

                await _enderecoRepository.Atualizar(instituicao.Endereco);

            }

            await _instituicaoRepository.Atualizar(instituicao);
        }



        public async Task Remover(Guid id)
        {
            await _instituicaoRepository.Remover(id);
        }
        public async Task<string> ObterAvatarAleatorioAsync()
        {
            string caminhoAvatares = Path.Combine("wwwroot", "assets", "avatar");

            if (!Directory.Exists(caminhoAvatares))
                throw new Exception("O diretório de avatares não foi encontrado.");

            var arquivos = Directory.GetFiles(caminhoAvatares, "*.jpg");

            if (arquivos.Length == 0)
                throw new Exception("Nenhuma imagem de avatar encontrada.");

            var random = new Random();
            string caminhoImagem = arquivos[random.Next(arquivos.Length)];

            string caminhoRelativo = caminhoImagem.Substring(caminhoImagem.IndexOf("wwwroot")).Replace("\\", "/");

            return await Task.FromResult("/" + caminhoRelativo.Replace("wwwroot/", ""));
        }


        public void Dispose()
        {
            _instituicaoRepository?.Dispose();
        }
    }
}