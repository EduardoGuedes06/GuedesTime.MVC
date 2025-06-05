using GuedesTime.Data.Context;
using GuedesTime.Data.Repository;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Utils;

namespace GuedesTime.Service.Services
{
    public class InstituicaoService : BaseService<Instituicao>, IInstituicaoService
    {
        private readonly IInstituicaoRepository _instituicaoRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public InstituicaoService(IInstituicaoRepository InstituicaoRepository,
                                  IEnderecoRepository enderecoRepository,
                                  INotificador notificador,
                                  MeuDbContext context,
                                  IPagedResultRepository<Instituicao> pagedRepository) : base(notificador, context, pagedRepository)
        {
            _instituicaoRepository = InstituicaoRepository;
            _enderecoRepository = enderecoRepository;
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

        public async Task<Instituicao> ObterDadosInstituicoesPorId(Guid instituicaoId)
        {
            return await _instituicaoRepository.ObterDadosInstituicoesPorId(instituicaoId);
        }

        public async Task<bool> VerificaUsuarioInstituicao(Guid usuarioId, Guid instituicaoId)
        {
            return await _instituicaoRepository.VerificaUsuarioInstituicao(usuarioId, instituicaoId);
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

        public async Task<Dictionary<Guid, DadosAgregadosInstituicao>> ObterCalculoGeralDosDadosDaInstituicao(List<Guid> instituicaoIds)
        {
            var result = new Dictionary<Guid, DadosAgregadosInstituicao>();

            foreach (var id in instituicaoIds)
            {
                var instituicao = await _instituicaoRepository.ObterDadosInstituicoesPorId(id, incluirInativos: false);

                if (instituicao == null) continue;

                var dados = new DadosAgregadosInstituicao
                {
                    TotalProfessores = instituicao.Professores?.Count ?? 0,
                    TotalTurmas = instituicao.Turmas?.Count ?? 0,
                    TotalSeries = instituicao.Series?.Count ?? 0,
                    TotalDisciplinas = instituicao.Disciplinas?.Count ?? 0,
                    TotalSalas = instituicao.Salas?.Count ?? 0,
                    TotalHorarios = instituicao.Horarios?.Count ?? 0,
                    TotalFeriados = instituicao.Feriados?.Count ?? 0
                };

                result.Add(id, dados);
            }

            return result;
        }


    }
}