namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarcolumnatipoempresaaempresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresas", "TipoEmpresa_Id", c => c.Short());
            CreateIndex("dbo.Empresas", "TipoEmpresa_Id");
            AddForeignKey("dbo.Empresas", "TipoEmpresa_Id", "dbo.TipoEmpresas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Empresas", "TipoEmpresa_Id", "dbo.TipoEmpresas");
            DropIndex("dbo.Empresas", new[] { "TipoEmpresa_Id" });
            DropColumn("dbo.Empresas", "TipoEmpresa_Id");
        }
    }
}
