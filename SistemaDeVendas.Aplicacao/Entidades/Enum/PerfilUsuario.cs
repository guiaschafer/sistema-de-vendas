using System.ComponentModel;

namespace SistemaDeVendas.Aplicacao.Entidades.Enum
{
    public enum PerfilUsuario
    {
        [Description("Administrador")]
        Administrador = 0,
        [Description("Cliente")]
        Cliente = 1,
        [Description("Gerente")]
        Gerente = 2,
        [Description("Vendedor")]
        Vendedor = 3,
    }
}
