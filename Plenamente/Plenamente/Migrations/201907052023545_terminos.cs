namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class terminos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Pers_Terminos", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Pers_Terminos");
        }
    }
}
