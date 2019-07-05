namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _checked : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Empresas", "TipoEmpresa_Id", "dbo.TipoEmpresas");
            DropIndex("dbo.Empresas", new[] { "TipoEmpresa_Id" });
            DropColumn("dbo.Empresas", "TipoEmpresa_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Empresas", "TipoEmpresa_Id", c => c.Short());
            CreateIndex("dbo.Empresas", "TipoEmpresa_Id");
            AddForeignKey("dbo.Empresas", "TipoEmpresa_Id", "dbo.TipoEmpresas", "Id");
        }
    }
}
