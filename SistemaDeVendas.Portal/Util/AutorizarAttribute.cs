using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaDeVendas.Aplicacao.Entidades.Enum;
using SistemaDeVendas.Aplicacao.Seguranca;

namespace SistemaDeVendas.Portal.Util
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AutorizarAttribute: AuthorizeAttribute
    {
        /// <summary>
        /// Representa os perfis que podem acessar este recurso
        /// </summary>
        public PerfilUsuario Perfis { get; set; }

        /// <summary>
        /// Ativa/Desativa este filtro
        /// </summary>
        public Boolean Ativo { get; set; }

        public AutorizarAttribute()
        {
            Ativo = true;
            Perfis = PerfilUsuario.Cliente;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!Ativo)
            {
                return true;
            }

            var principal = httpContext.User as Principal;
            if (principal == null || !principal.Identity.IsAuthenticated)
                return false;

            bool acessoPermitido = true;
            if (Perfis != PerfilUsuario.Administrador)
            {
                acessoPermitido = principal.IsInRole(PerfilUsuario.Administrador | Perfis);
            }

            //TODO: Validar a existencia do usuario
            return ((Identidade)principal.Identity).Token.Valido && acessoPermitido;
        }
    }
}