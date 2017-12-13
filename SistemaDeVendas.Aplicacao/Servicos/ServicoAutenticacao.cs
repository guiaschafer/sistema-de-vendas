using SistemaDeVendas.Aplicacao.Dto;
using SistemaDeVendas.Aplicacao.Entidades;
using SistemaDeVendas.Aplicacao.Infraestrutura;
using SistemaDeVendas.Aplicacao.Util;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using SistemaDeVendas.Aplicacao.Seguranca;

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

        public HttpCookie GerarCookieComToken(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            var token = GerarToken(usuario);

            var lembrarSenha = false;
            var ticket = new FormsAuthenticationTicket(
                1,
                Guid.NewGuid().ToString(),
                DateTime.Now,
                token.ExpiraEm,
                lembrarSenha,
                token.GerarJwt(),
                FormsAuthentication.FormsCookiePath);

            var encTicket = FormsAuthentication.Encrypt(ticket);
            return new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        }

        public Token GerarToken(Usuario usuario)
        {
            var token = new Token();
            token.ExpiraEm = DateTime.UtcNow.AddHours(1);

            token.AdicionarClaim(new Claim(Claims.Id, Convert.ToString(usuario.Id)));
            token.AdicionarClaim(new Claim(Claims.Nome, usuario.Nome));
            token.AdicionarClaim(new Claim(Claims.Login, usuario.Login));

            if (usuario.Perfis != null)
            {
                foreach (var perfil in usuario.Perfis)
                {
                    var perfilClaim = new Claim(ClaimTypes.Role, perfil.Tipo.ObterValor().ToString());
                    token.AdicionarClaim(perfilClaim);
                }
            }

            return token;
        }

        public Usuario Autenticar(Token token)
        {
            if (token == null || !token.Valido)
            {
                return null;
            }

            var idUsuarioClaim = token.Claims
                .FirstOrDefault(claim => claim.Type.Equals(Claims.Id));

            int idUsuario;
            int.TryParse(idUsuarioClaim?.Value, out idUsuario);

            return contexo.Usuarios.Where(x => x.Id == idUsuario).FirstOrDefault();
        }

        public Principal CriarIdentidadePrincipal(Token token)
        {
            var identidade = new Identidade(token);
            var principal = new Principal(identidade);
            return principal;
        }

        public Principal CriarIdentidadePrincipalVazia()
        {
            var identidade = new Identidade();
            var principal = new Principal(identidade);
            return principal;
        }
    }
}
