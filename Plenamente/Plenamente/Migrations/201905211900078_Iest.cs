namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Iest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemEstandars", "Iest_Video", c => c.String());
            AddColumn("dbo.ItemEstandars", "Iest_Recurso", c => c.String());
            AddColumn("dbo.ItemEstandars", "Iest_Rescursob", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemEstandars", "Iest_Rescursob");
            DropColumn("dbo.ItemEstandars", "Iest_Recurso");
            DropColumn("dbo.ItemEstandars", "Iest_Video");
        }
    }
}
