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
            Mapper.CreateMap<Produto, ProdutoDto>();
            Mapper.CreateMap<Usuario, UsuarioDto>();
            Mapper.CreateMap<Perfil, PerfilDto>();
            Mapper.CreateMap<Cliente, ClienteDto>()
                .ForMember(c=> c.CodigoSeguracaCript, op => op.MapFrom(i => i.CodigoSeguranca))
                .ForMember(c => c.NumeroCartaoCript, op => op.MapFrom(i => i.NumeroCartao));

        }
    }
}
