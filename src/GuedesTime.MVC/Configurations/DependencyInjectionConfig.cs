
using GuedesTime.Data.Context;
using GuedesTime.Data.Repository;
using GuedesTime.Domain.Intefaces;
using GuedesTime.Domain.Notificacoes;
using GuedesTime.MVC.Extensions;
using GuedesTime.MVC.Interfaces;
using GuedesTime.MVC.Services;
using GuedesTime.Service.Services;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.Design;

namespace GuedesTime.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmailSenderGripGrip, EmailSenderGrip>();

            services.AddScoped<MeuDbContext>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IEmailSenderGripGrip, EmailSenderGrip>();

            // Services
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<ITarefasService, TarefasService>();
            services.AddScoped<ISalaService, SalaService>();
            services.AddScoped<IRestricaoService, RestricaoService>();
            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IPlanejamentoDeAulaService, PlanejamentoDeAulaService>();
            services.AddScoped<IHorarioService, HorarioService>();
            services.AddScoped<IHistoricoExportacaoService, HistoricoExportacaoService>();
            services.AddScoped<IDisciplinaService, DisciplinaService>();
            services.AddScoped<IConfiguracoesGenericasService, ConfiguracoesGenericasService>();
            services.AddScoped<IAtividadesService, AtividadesService>();
            services.AddScoped<IFeriadoService, FeriadoService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IInstituicaoService, InstituicaoService>();


            // Repositories
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<ITarefasRepository, TarefasRepository>();
            services.AddScoped<ISalaRepository, SalaRepository>();
            services.AddScoped<IRestricaoRepository, RestricaoRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IPlanejamentoDeAulaRepository, PlanejamentoDeAulaRepository>();
            services.AddScoped<IHorarioRepository, HorarioRepository>();
            services.AddScoped<IHistoricoExportacaoRepository, HistoricoExportacaoRepository>();
            services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
            services.AddScoped<IConfiguracoesGenericasRepository, ConfiguracoesGenericasRepository>();
            services.AddScoped<IAtividadesRepository, AtividadesRepository>();
            services.AddScoped<IFeriadoRepository, FeriadoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();

            return services;
        }
    }
}