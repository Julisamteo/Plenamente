namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegistroUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Pers_Licencia", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Pers_LicVence", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Pers_LicVence", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Pers_Licencia", c => c.Int(nullable: false));
        }
    }
}
