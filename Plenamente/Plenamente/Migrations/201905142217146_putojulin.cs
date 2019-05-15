namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class putojulin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Encuestas", "Encu_Nombre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Encuestas", "Encu_Nombre");
        }
    }
}
