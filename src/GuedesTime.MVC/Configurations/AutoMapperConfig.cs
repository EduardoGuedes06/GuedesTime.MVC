using AutoMapper;
using GuedesTime.Domain.Models;
using GuedesTime.MVC.ViewModels;
using GuedesTime.Service.Models;

namespace GuedesTime.MVC.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Turma, TurmaViewModel>().ReverseMap();
            CreateMap<Tarefas, TarefasViewModel>().ReverseMap();
            CreateMap<Sala, SalaViewModel>().ReverseMap();
            CreateMap<Restricao, RestricaoViewModel>().ReverseMap();
            CreateMap<Professor, ProfessorViewModel>().ReverseMap();
            CreateMap<PlanejamentoDeAula, PlanejamentoDeAulaViewModel>().ReverseMap();
            CreateMap<Horario, HorarioViewModel>().ReverseMap();
            CreateMap<HistoricoExportacao, HistoricoExportacaoViewModel>().ReverseMap();
            CreateMap<Disciplina, DisciplinaViewModel>().ReverseMap();
            CreateMap<ConfiguracoesGenericas, ConfiguracoesGenericasViewModel>().ReverseMap();
            CreateMap<Atividades, AtividadesViewModel>().ReverseMap();
            CreateMap<Log, LogViewModel>().ReverseMap();
            CreateMap<Instituicao, InstituicaoViewModel>().ReverseMap();
            CreateMap<Feriado, FeriadoViewModel>().ReverseMap();

            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}