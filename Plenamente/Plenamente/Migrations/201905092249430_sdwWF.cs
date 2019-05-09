namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdwWF : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Respuestas", "Qure_Id", "dbo.QuemRespuestas");
            DropIndex("dbo.Respuestas", new[] { "Qure_Id" });
            CreateTable(
                "dbo.TipoRespuestas",
                c => new
                    {
                        Quem_Id = c.Int(nullable: false, identity: true),
                        Quem_Nom = c.String(),
                    })
                .PrimaryKey(t => t.Quem_Id);
            
            AddColumn("dbo.Respuestas", "Quem_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Respuestas", "Quem_Id");
            AddForeignKey("dbo.Respuestas", "Quem_Id", "dbo.TipoRespuestas", "Quem_Id");
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
            DropForeignKey("dbo.Respuestas", "Quem_Id", "dbo.TipoRespuestas");
            DropIndex("dbo.Respuestas", new[] { "Quem_Id" });
            DropColumn("dbo.Respuestas", "Quem_Id");
            DropTable("dbo.TipoRespuestas");
            CreateIndex("dbo.Respuestas", "Qure_Id");
            AddForeignKey("dbo.Respuestas", "Qure_Id", "dbo.QuemRespuestas", "Qure_Id");
        }
    }
}
