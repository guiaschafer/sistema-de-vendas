namespace SistemaDeVendas.Aplicacao.Infraestrutura
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NovasEntidades : DbMigration
    {
        public override void Up()
        {
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
                "dbo.UsuarioPerfils",
                c => new
                    {
                        Usuario_Id = c.Int(nullable: false),
                        Perfil_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_Id, t.Perfil_Id })
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id, cascadeDelete: true)
                .ForeignKey("dbo.Perfils", t => t.Perfil_Id, cascadeDelete: true)
                .Index(t => t.Usuario_Id)
                .Index(t => t.Perfil_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioPerfils", "Perfil_Id", "dbo.Perfils");
            DropForeignKey("dbo.UsuarioPerfils", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.UsuarioPerfils", new[] { "Perfil_Id" });
            DropIndex("dbo.UsuarioPerfils", new[] { "Usuario_Id" });
            DropTable("dbo.UsuarioPerfils");
            DropTable("dbo.Perfils");
        }
    }
}
