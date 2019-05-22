namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posibleCorrecion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Respuestas", "Quem_Id", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Respuestas", "Quem_Id", c => c.Int(nullable: false));
        }
    }
}
