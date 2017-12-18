namespace SistemaDeVendas.Aplicacao.Infraestrutura
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Sobrenome = c.String(),
                        Cpf = c.String(),
                        Telefone = c.String(),
                        Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.USUARIO", t => t.Usuario_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.USUARIO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.Binary(),
                        Login = c.String(),
                        Senha = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Perfils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemPedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Item_Id = c.Int(),
                        Pedido_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtoes", t => t.Item_Id)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_Id)
                .Index(t => t.Item_Id)
                .Index(t => t.Pedido_Id);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        UnidadeMedida = c.String(),
                        ValorUnidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .Index(t => t.Cliente_Id);
            
            CreateTable(
                "dbo.PerfilUsuarios",
                c => new
                    {
                        Perfil_Id = c.Int(nullable: false),
                        Usuario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Perfil_Id, t.Usuario_Id })
                .ForeignKey("dbo.Perfils", t => t.Perfil_Id, cascadeDelete: true)
                .ForeignKey("dbo.USUARIO", t => t.Usuario_Id, cascadeDelete: true)
                .Index(t => t.Perfil_Id)
                .Index(t => t.Usuario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemPedidoes", "Pedido_Id", "dbo.Pedidoes");
            DropForeignKey("dbo.Pedidoes", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.ItemPedidoes", "Item_Id", "dbo.Produtoes");
            DropForeignKey("dbo.Clientes", "Usuario_Id", "dbo.USUARIO");
            DropForeignKey("dbo.PerfilUsuarios", "Usuario_Id", "dbo.USUARIO");
            DropForeignKey("dbo.PerfilUsuarios", "Perfil_Id", "dbo.Perfils");
            DropIndex("dbo.PerfilUsuarios", new[] { "Usuario_Id" });
            DropIndex("dbo.PerfilUsuarios", new[] { "Perfil_Id" });
            DropIndex("dbo.Pedidoes", new[] { "Cliente_Id" });
            DropIndex("dbo.ItemPedidoes", new[] { "Pedido_Id" });
            DropIndex("dbo.ItemPedidoes", new[] { "Item_Id" });
            DropIndex("dbo.Clientes", new[] { "Usuario_Id" });
            DropTable("dbo.PerfilUsuarios");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Produtoes");
            DropTable("dbo.ItemPedidoes");
            DropTable("dbo.Perfils");
            DropTable("dbo.USUARIO");
            DropTable("dbo.Clientes");
        }
    }
}
