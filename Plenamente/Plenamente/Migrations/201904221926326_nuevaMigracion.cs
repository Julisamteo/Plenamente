namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nuevaMigracion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cumplimientoes", "Cump_Evidencia", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cumplimientoes", "Cump_Evidencia", c => c.Binary());
        }
    }
}
