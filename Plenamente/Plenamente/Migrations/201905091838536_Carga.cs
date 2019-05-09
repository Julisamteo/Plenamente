namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Carga : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Respuestas", "Qure_Id", "dbo.QuemRespuestas");
            //DropIndex("dbo.Respuestas", new[] { "Qure_Id" });
            //CreateTable(
            //    "dbo.TipoRespuestas",
            //    c => new
            //        {
            //            Tres_Id = c.Int(nullable: false, identity: true),
            //            Tres_Nom = c.String(),
            //        })
            //    .PrimaryKey(t => t.Tres_Id);
            
            //AddColumn("dbo.Respuestas", "Tres_Id", c => c.Int());
            //CreateIndex("dbo.Respuestas", "Tres_Id");
            //AddForeignKey("dbo.Respuestas", "Tres_Id", "dbo.TipoRespuestas", "Tres_Id");
            //DropColumn("dbo.Respuestas", "Qure_Id");
            //DropTable("dbo.QuemRespuestas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuemRespuestas",
                c => new
                    {
                        Qure_Id = c.Int(nullable: false, identity: true),
                        Qure_Nom = c.String(),
                    })
                .PrimaryKey(t => t.Qure_Id);
            
            AddColumn("dbo.Respuestas", "Qure_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Respuestas", "Tres_Id", "dbo.TipoRespuestas");
            DropIndex("dbo.Respuestas", new[] { "Tres_Id" });
            DropColumn("dbo.Respuestas", "Tres_Id");
            DropTable("dbo.TipoRespuestas");
            CreateIndex("dbo.Respuestas", "Qure_Id");
            AddForeignKey("dbo.Respuestas", "Qure_Id", "dbo.QuemRespuestas", "Qure_Id");
        }
    }
}
