using AutoMapper;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;

namespace SistemaDeVendas.Aplicacao.AutoMapper
{
    public class EntidadeToDtoMappingProfile : Profile
    {
        public EntidadeToDtoMappingProfile() : base("DomainToViewModelMappings")
        {

        }

        protected override void Configure()
        {
            Mapper.CreateMap<Usuario, UsuarioDto>();
            Mapper.CreateMap<Perfil, PerfilDto>();
            Mapper.CreateMap<Cliente, ClienteDto>();

        }
    }
}
