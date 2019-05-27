namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRelationCiudad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Ciud_Id", "dbo.Ciudads");
            DropIndex("dbo.AspNetUsers", new[] { "Ciud_Id" });
            DropColumn("dbo.AspNetUsers", "Ciud_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Ciud_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Ciud_Id");
            AddForeignKey("dbo.AspNetUsers", "Ciud_Id", "dbo.Ciudads", "Ciud_Id");
        }
    }
}
