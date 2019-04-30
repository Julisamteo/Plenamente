namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cumplimientoes", "Cump_Evidencia", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cumplimientoes", "Cump_Evidencia", c => c.String());
        }
    }
}
