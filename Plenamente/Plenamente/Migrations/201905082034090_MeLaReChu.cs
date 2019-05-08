namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeLaReChu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cumplimientoes", "Cump_Contenido", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cumplimientoes", "Cump_Contenido");
        }
    }
}
