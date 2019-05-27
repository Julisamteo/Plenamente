namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveResultsRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resultadoes", "Pregunta_Preg_Id", "dbo.Preguntas");
            DropForeignKey("dbo.TipoRespuestas", "Respuesta_Resp_Id", "dbo.Respuestas");
            DropIndex("dbo.Resultadoes", new[] { "Pregunta_Preg_Id" });
            DropIndex("dbo.TipoRespuestas", new[] { "Respuesta_Resp_Id" });
            DropColumn("dbo.Resultadoes", "Pregunta_Preg_Id");
            DropTable("dbo.TipoRespuestas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TipoRespuestas",
                c => new
                    {
                        Quem_Id = c.Int(nullable: false, identity: true),
                        Quem_Nom = c.String(),
                        Respuesta_Resp_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Quem_Id);
            
            AddColumn("dbo.Resultadoes", "Pregunta_Preg_Id", c => c.Int());
            CreateIndex("dbo.TipoRespuestas", "Respuesta_Resp_Id");
            CreateIndex("dbo.Resultadoes", "Pregunta_Preg_Id");
            AddForeignKey("dbo.TipoRespuestas", "Respuesta_Resp_Id", "dbo.Respuestas", "Resp_Id");
            AddForeignKey("dbo.Resultadoes", "Pregunta_Preg_Id", "dbo.Preguntas", "Preg_Id");
        }
    }
}
