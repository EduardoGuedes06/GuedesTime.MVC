using AutoMapper;
using GuedesTime.Domain.Models;
using GuedesTime.Domain.Models.Enums;
using GuedesTime.Domain.Models.Utils;
using GuedesTime.MVC.ViewModels;
using GuedesTime.MVC.ViewModels.Enum;
using GuedesTime.MVC.ViewModels.Utils;

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
            CreateMap<Serie, LogViewModel>().ReverseMap();

			//CreateMap<SerieViewModel, Serie>()
	  //      .ForMember(dest => dest.SerieUnica, opt => opt.Ignore())
	  //      .ForMember(dest => dest.SeriesMultiplas, opt => opt.Ignore())
	  //      .ForMember(dest => dest.ListaTipoEnsino, opt => opt.Ignore());


			CreateMap<Instituicao, InstituicaoViewModel>().ReverseMap();
            CreateMap<Feriado, FeriadoViewModel>().ReverseMap();
            CreateMap<DisciplinaProfessor, DisciplinaProfessorViewModel>().ReverseMap();
            CreateMap<PlanejamentoDeAulaItem, PlanejamentoDeAulaItemViewModel>().ReverseMap();
            CreateMap<Serie, SerieViewModel>().ReverseMap();
			CreateMap<EnumTipoEnsino, EnumTipoEnsinoViewModel>().ReverseMap();
			CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<DisciplinaSerie, DisciplinaSerieViewModel>().ReverseMap();

            CreateMap<DadosAgregadosInstituicao, DadosAgregadosInstituicaoViewModel>().ReverseMap();
            CreateMap(typeof(PagedResult<>), typeof(PagedViewModel<>));
        }
    }
}