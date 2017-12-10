using System.ComponentModel;

namespace SistemaDeVendas.Aplicacao.Entidades.Enum
{
    public enum PerfilUsuario
    {
        [Description("Administrador")]
        Administrador = 0,

        [Description("Usuário")]
        Usuario = 1
    }
}
