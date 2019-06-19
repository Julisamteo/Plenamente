namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4Columitemstandar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemEstandars", "Iest_Rescursoc", c => c.String());
            AddColumn("dbo.ItemEstandars", "Iest_Rescursod", c => c.String());
            AddColumn("dbo.ItemEstandars", "Iest_Rescursoe", c => c.String());
            AddColumn("dbo.ItemEstandars", "Iest_Rescursof", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemEstandars", "Iest_Rescursof");
            DropColumn("dbo.ItemEstandars", "Iest_Rescursoe");
            DropColumn("dbo.ItemEstandars", "Iest_Rescursod");
            DropColumn("dbo.ItemEstandars", "Iest_Rescursoc");
        }
    }
}
