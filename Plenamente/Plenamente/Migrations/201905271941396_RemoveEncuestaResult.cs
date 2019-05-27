namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEncuestaResult : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resultadoes", "Encu_Id", "dbo.Encuestas");
            DropIndex("dbo.Resultadoes", new[] { "Encu_Id" });
            DropColumn("dbo.Resultadoes", "Encu_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resultadoes", "Encu_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Resultadoes", "Encu_Id");
            AddForeignKey("dbo.Resultadoes", "Encu_Id", "dbo.Encuestas", "Encu_Id");
        }
    }
}
