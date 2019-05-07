namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeCagoEnLaPuta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Respuestas", "Resp_Tipo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Respuestas", "Resp_Tipo");
        }
    }
}
