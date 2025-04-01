
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

            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();


            return services;
        }
    }
}