using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;
using SistemaDeVendas.Aplicacao.Infraestrutura;
using SistemaDeVendas.Aplicacao.Util;
using System;
using System.Linq;

namespace SistemaDeVendas.Aplicacao.Servicos
{
    public class ServicoAutenticacao
    {
        private Contexto contexo = new Contexto();

        public Usuario Autenticar(AutenticacaoDto login)
        {
            if (string.IsNullOrEmpty(login.Login))
            {
                throw new ArgumentNullException("É necessário informar o login.");
            }

            if (string.IsNullOrEmpty(login.Senha))
            {
                throw new ArgumentNullException("É necessário informar a senha.");
            }

            var usuario = contexo.Usuarios.Where(x => x.Login == login.Login).FirstOrDefault();

            var hashSenha = Utils.GenerateSHA512String(login.Senha + usuario.Salt);

            if (hashSenha == usuario.Senha)
            {
                return usuario;

            }

            return null;
        }
    }
}
