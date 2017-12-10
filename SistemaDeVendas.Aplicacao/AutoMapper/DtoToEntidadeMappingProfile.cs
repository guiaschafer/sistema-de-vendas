using AutoMapper;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;

namespace SistemaDeVendas.Aplicacao.AutoMapper
{
    public class DtoToEntidadeMappingProfile: Profile
    {
        public DtoToEntidadeMappingProfile() : base("ViewModelToDomainMappings")
        {

        }

        protected override void Configure()
        {
            Mapper.CreateMap<UsuarioDto, Usuario>();
        }
    }
}
