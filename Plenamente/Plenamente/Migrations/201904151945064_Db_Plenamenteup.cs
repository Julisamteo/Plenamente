namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db_Plenamenteup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ObjEmpresas", "Oemp_Meta", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ObjEmpresas", "Oemp_Meta");
        }
    }
}
