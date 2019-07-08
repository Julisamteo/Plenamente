namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Empresas", "Empr_NewNit", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Empresas", new[] { "Empr_NewNit" });
        }
    }
}
