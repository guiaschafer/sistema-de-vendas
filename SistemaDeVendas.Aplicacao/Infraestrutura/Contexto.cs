using System.Data.Entity;
using SistemaDeVendas.Aplicacao.Entidades;

namespace SistemaDeVendas.Aplicacao.Infraestrutura
{
    public class Contexto : DbContext
    {
        public Contexto() : base()
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis{ get; set; }
    }
}


