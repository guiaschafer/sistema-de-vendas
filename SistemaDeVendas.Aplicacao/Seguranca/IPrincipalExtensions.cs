using SistemaDeVendas.Aplicacao.Entidades.Enum;
using System.Security.Principal;

namespace SistemaDeVendas.Aplicacao.Seguranca
{
    public static class IPrincipalExtensions
    {
        public static Token ObterTokenAtual(this IPrincipal principal)
        {
            return (principal.Identity as Identidade)?.Token;
        }

        public static bool IsInRole(this IPrincipal principal, PerfilUsuario perfil)
        {
            return (principal as Principal).IsInRole(perfil);
        }
    }
}
