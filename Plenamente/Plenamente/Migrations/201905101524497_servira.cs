namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servira : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Respuestas", name: "QuemRespuesta_Quem_Id", newName: "respuestasQuemadas_Quem_Id");
            RenameIndex(table: "dbo.Respuestas", name: "IX_QuemRespuesta_Quem_Id", newName: "IX_respuestasQuemadas_Quem_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Respuestas", name: "IX_respuestasQuemadas_Quem_Id", newName: "IX_QuemRespuesta_Quem_Id");
            RenameColumn(table: "dbo.Respuestas", name: "respuestasQuemadas_Quem_Id", newName: "QuemRespuesta_Quem_Id");
        }
    }
}
