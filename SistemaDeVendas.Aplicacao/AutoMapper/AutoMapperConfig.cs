using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SistemaDeVendas.Aplicacao.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntidadeToDtoMappingProfile>();
                x.AddProfile<DtoToEntidadeMappingProfile>();
            });

        }

        public static IEnumerable<Profile> GetProfiles()
        {
            var profiles = new List<Profile>();
            profiles.Add(new EntidadeToDtoMappingProfile());
            profiles.Add(new DtoToEntidadeMappingProfile());
            return profiles;
        }
    }
}
