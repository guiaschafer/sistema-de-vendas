﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;
using SistemaDeVendas.Aplicacao.Entidades.Enum;
using SistemaDeVendas.Aplicacao.Infraestrutura;
using SistemaDeVendas.Aplicacao.Util;

namespace SistemaDeVendas.Aplicacao.Servicos
{
    public class ServicoUsuario
    {
        private Contexto contexo = new Contexto();

        public void Cadastrar(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                throw new ArgumentException(nameof(usuarioDto));
            }

            var usuario = Mapper.Map<UsuarioDto, Usuario>(usuarioDto);

            usuario.Salt = Utils.GetSalt();
            usuario.Senha = Utils.GenerateSHA512String(usuario.Senha + usuario.Salt);
            usuario.Perfis = new List<Perfil>()
            {
                contexo.Perfis.Where(x => x.Tipo == PerfilUsuario.Usuario).FirstOrDefault()
            };
            contexo.Usuarios.Add(usuario);
            contexo.SaveChanges();
        }

        //public UsuarioDto Obter(int id)
        //{
        //    var usuario = contexo.Usuarios.Where(u => u.Id == id).FirstOrDefault();

        //    return Mapper.Map<Usuario, UsuarioDto>(usuario);
        //}

        //public List<UsuarioDto> Obtertodos()
        //{
        //    var usuario = contexo.Usuarios.ToList();

        //    return Mapper.Map<List<Usuario>, List<UsuarioDto>>(usuario);
        //}
    }
}