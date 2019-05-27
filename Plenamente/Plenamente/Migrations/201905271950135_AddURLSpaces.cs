namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddURLSpaces : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Politicas", "Poli_Nom", c => c.String());
            AddColumn("dbo.ReglaHigienes", "Rhig_Nom", c => c.String());
            AddColumn("dbo.ReglaInternoes", "Rint_Nom", c => c.String());
            AlterColumn("dbo.Politicas", "Poli_Archivo", c => c.String());
            AlterColumn("dbo.ReglaHigienes", "Rhig_Archivo", c => c.String());
            AlterColumn("dbo.ReglaInternoes", "Rint_Archivo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReglaInternoes", "Rint_Archivo", c => c.Binary());
            AlterColumn("dbo.ReglaHigienes", "Rhig_Archivo", c => c.Binary());
            AlterColumn("dbo.Politicas", "Poli_Archivo", c => c.Binary());
            DropColumn("dbo.ReglaInternoes", "Rint_Nom");
            DropColumn("dbo.ReglaHigienes", "Rhig_Nom");
            DropColumn("dbo.Politicas", "Poli_Nom");
        }
    }
}
