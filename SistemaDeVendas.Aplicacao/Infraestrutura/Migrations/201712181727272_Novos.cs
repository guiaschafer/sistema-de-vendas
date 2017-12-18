namespace SistemaDeVendas.Aplicacao.Infraestrutura
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Novos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "Quantidade", c => c.Int(nullable: false));
            AddColumn("dbo.Pedidoes", "Item_Id", c => c.Int());
            CreateIndex("dbo.Pedidoes", "Item_Id");
            AddForeignKey("dbo.Pedidoes", "Item_Id", "dbo.Produtoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "Item_Id", "dbo.Produtoes");
            DropIndex("dbo.Pedidoes", new[] { "Item_Id" });
            DropColumn("dbo.Pedidoes", "Item_Id");
            DropColumn("dbo.Pedidoes", "Quantidade");
        }
    }
}
