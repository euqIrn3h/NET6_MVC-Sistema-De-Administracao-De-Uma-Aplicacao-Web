using Admin_MVC.Models;
using AutoMapper;
using Admin_MVC.Business.Dtos;

namespace Admin_MVC.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioDto, UsuarioData>();
            CreateMap<UsuarioData, UsuarioDto>();
            CreateMap<UsuarioData, DadosDoUsuarioDto>();
            CreateMap<DadosDoUsuarioDto, UsuarioData>();
            CreateMap<ProdutoData, ProdutoDto>();
            CreateMap<ProdutoDto, ProdutoData>();
            CreateMap<UsuarioData, AlteraSenhaDto>();
            CreateMap<AlteraSenhaDto, UsuarioData>();
            CreateMap<UsuarioData, UsuarioApiDto>();
        }
    }
}