using System.Data.Entity;
using SistemaDeVendas.Aplicacao.Entidades;
using SistemaDeVendas.Aplicacao.Infraestrutura.Mapeamentos;

namespace SistemaDeVendas.Aplicacao.Infraestrutura
{
    public class Contexto : DbContext
    {
        public Contexto() : base()
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UsuarioMap());
        }
    }
}


