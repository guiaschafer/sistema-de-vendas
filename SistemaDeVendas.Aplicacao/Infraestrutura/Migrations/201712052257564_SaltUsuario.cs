namespace SistemaDeVendas.Aplicacao.Infraestrutura
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaltUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "Salt");
        }
    }
}
