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
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}