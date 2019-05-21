namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorreccionForanea : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AutoEvaluacions", "AutoEvaluacion_Auev_Id", "dbo.AutoEvaluacions");
            DropIndex("dbo.AutoEvaluacions", new[] { "AutoEvaluacion_Auev_Id" });
            DropColumn("dbo.AutoEvaluacions", "AutoEvaluacion_Auev_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AutoEvaluacions", "AutoEvaluacion_Auev_Id", c => c.Int());
            CreateIndex("dbo.AutoEvaluacions", "AutoEvaluacion_Auev_Id");
            AddForeignKey("dbo.AutoEvaluacions", "AutoEvaluacion_Auev_Id", "dbo.AutoEvaluacions", "Auev_Id");
        }
    }
}
