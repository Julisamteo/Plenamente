namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Pers_Terminos");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Pers_Terminos", c => c.Boolean(nullable: false));
        }
    }
}
