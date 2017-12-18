using AutoMapper;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;
using SistemaDeVendas.Aplicacao.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendas.Aplicacao.Servicos
{
    public class ServicoPerfil
    {
        private Contexto contexo = new Contexto();

        public List<PerfilDto> ObterTodos()
        {
            return Mapper.Map<List<Perfil>, List<PerfilDto>>(contexo.Perfis.Where(p => 1 == 1).ToList());
        }
    }
}
