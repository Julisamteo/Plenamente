namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cumpli : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cumplimientoes", "Cump_Cumple", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cumplimientoes", "Cump_Nocumple", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cumplimientoes", "Cump_Justifica", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cumplimientoes", "Cump_Nojustifica", c => c.Boolean(nullable: false));
            AddColumn("dbo.Cumplimientoes", "Cump_Observ", c => c.String());
            DropColumn("dbo.ItemEstandars", "Iest_Cumple");
            DropColumn("dbo.ItemEstandars", "Iest_Nocumple");
            DropColumn("dbo.ItemEstandars", "Iest_Justifica");
            DropColumn("dbo.ItemEstandars", "Iest_Nojustifica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemEstandars", "Iest_Nojustifica", c => c.Boolean(nullable: false));
            AddColumn("dbo.ItemEstandars", "Iest_Justifica", c => c.Boolean(nullable: false));
            AddColumn("dbo.ItemEstandars", "Iest_Nocumple", c => c.Boolean(nullable: false));
            AddColumn("dbo.ItemEstandars", "Iest_Cumple", c => c.Boolean(nullable: false));
            DropColumn("dbo.Cumplimientoes", "Cump_Observ");
            DropColumn("dbo.Cumplimientoes", "Cump_Nojustifica");
            DropColumn("dbo.Cumplimientoes", "Cump_Justifica");
            DropColumn("dbo.Cumplimientoes", "Cump_Nocumple");
            DropColumn("dbo.Cumplimientoes", "Cump_Cumple");
        }
    }
}
