namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Pers_terminos", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Pers_terminos", c => c.Boolean(nullable: false));
        }
    }
}
