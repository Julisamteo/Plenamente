namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProgamacionTareas", "Finalizada");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProgamacionTareas", "Finalizada", c => c.Boolean(nullable: false));
        }
    }
}
