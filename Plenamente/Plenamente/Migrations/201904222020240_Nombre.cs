namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nombre : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Criterios", "Crit_Nom", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Criterios", "Crit_Nom", c => c.Int(nullable: false));
        }
    }
}
