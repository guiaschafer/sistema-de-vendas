using System;
using System.ComponentModel;

namespace SistemaDeVendas.Aplicacao.Entidades.Enum
{
    [Flags]
    public enum PerfilUsuario
    {
        [Description("Administrador")]
        Administrador = 0,
        [Description("Cliente")]
        Cliente = 1,
        [Description("Gerente")]
        Gerente = 2,
        [Description("Vendedor")]
        Vendedor = 4,
        [Description("Publico")]
        Publico = 8
    }
}
