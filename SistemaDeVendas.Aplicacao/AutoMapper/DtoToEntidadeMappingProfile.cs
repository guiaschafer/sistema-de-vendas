using AutoMapper;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;

namespace SistemaDeVendas.Aplicacao.AutoMapper
{
    public class DtoToEntidadeMappingProfile : Profile
    {
        public DtoToEntidadeMappingProfile() : base("ViewModelToDomainMappings")
        {

        }

        protected override void Configure()
        {
            Mapper.CreateMap<ProdutoDto, Produto>();

            Mapper.CreateMap<UsuarioDto, Usuario>();
            Mapper.CreateMap<ClienteDto, Cliente>()
                .ForMember(c => c.NumeroCartao, op => op.Ignore())
                .ForMember(c=> c.CodigoSeguranca, op => op.Ignore());
        }
    }
}
