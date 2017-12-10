using SistemaDeVendas.Aplicacao.Entidades.Enum;
using System.Collections.Generic;

namespace SistemaDeVendas.Aplicacao.Entidades
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public PerfilUsuario Tipo { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
