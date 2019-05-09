namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HAGALE : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Respuestas", "Quem_Id", "dbo.TipoRespuestas");
            DropIndex("dbo.Respuestas", new[] { "Quem_Id" });
            AddColumn("dbo.Respuestas", "respuestasQuemadas_Quem_Id", c => c.Int());
            AddColumn("dbo.TipoRespuestas", "Respuesta_Resp_Id", c => c.Int());
            CreateIndex("dbo.Respuestas", "respuestasQuemadas_Quem_Id");
            CreateIndex("dbo.TipoRespuestas", "Respuesta_Resp_Id");
            AddForeignKey("dbo.TipoRespuestas", "Respuesta_Resp_Id", "dbo.Respuestas", "Resp_Id");
            AddForeignKey("dbo.Respuestas", "respuestasQuemadas_Quem_Id", "dbo.TipoRespuestas", "Quem_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Respuestas", "respuestasQuemadas_Quem_Id", "dbo.TipoRespuestas");
            DropForeignKey("dbo.TipoRespuestas", "Respuesta_Resp_Id", "dbo.Respuestas");
            DropIndex("dbo.TipoRespuestas", new[] { "Respuesta_Resp_Id" });
            DropIndex("dbo.Respuestas", new[] { "respuestasQuemadas_Quem_Id" });
            DropColumn("dbo.TipoRespuestas", "Respuesta_Resp_Id");
            DropColumn("dbo.Respuestas", "respuestasQuemadas_Quem_Id");
            CreateIndex("dbo.Respuestas", "Quem_Id");
            AddForeignKey("dbo.Respuestas", "Quem_Id", "dbo.TipoRespuestas", "Quem_Id");
        }
    }
}
