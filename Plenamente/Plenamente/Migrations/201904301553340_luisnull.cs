namespace Plenamente.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class luisnull : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Tdoc_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Sciu_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Ciud_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Aemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cate_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Gene_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Jemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Tvin_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Eps_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Afp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Arl_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Empr_Nit" });
            DropIndex("dbo.AspNetUsers", new[] { "Espe_Id" });
            AlterColumn("dbo.AspNetUsers", "Tdoc_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Sciu_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Ciud_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Cemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Aemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Cate_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Gene_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Jemp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Tvin_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Eps_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Afp_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Arl_Id", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Empr_Nit", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "Espe_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Tdoc_Id");
            CreateIndex("dbo.AspNetUsers", "Sciu_Id");
            CreateIndex("dbo.AspNetUsers", "Ciud_Id");
            CreateIndex("dbo.AspNetUsers", "Cemp_Id");
            CreateIndex("dbo.AspNetUsers", "Aemp_Id");
            CreateIndex("dbo.AspNetUsers", "Cate_Id");
            CreateIndex("dbo.AspNetUsers", "Gene_Id");
            CreateIndex("dbo.AspNetUsers", "Jemp_Id");
            CreateIndex("dbo.AspNetUsers", "Tvin_Id");
            CreateIndex("dbo.AspNetUsers", "Eps_Id");
            CreateIndex("dbo.AspNetUsers", "Afp_Id");
            CreateIndex("dbo.AspNetUsers", "Arl_Id");
            CreateIndex("dbo.AspNetUsers", "Empr_Nit");
            CreateIndex("dbo.AspNetUsers", "Espe_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Espe_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Empr_Nit" });
            DropIndex("dbo.AspNetUsers", new[] { "Arl_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Afp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Eps_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Tvin_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Jemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Gene_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cate_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Aemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Cemp_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Ciud_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Sciu_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Tdoc_Id" });
            AlterColumn("dbo.AspNetUsers", "Espe_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Empr_Nit", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Arl_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Afp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Eps_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Tvin_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Jemp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Gene_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Cate_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Aemp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Cemp_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Ciud_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Sciu_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Tdoc_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "Espe_Id");
            CreateIndex("dbo.AspNetUsers", "Empr_Nit");
            CreateIndex("dbo.AspNetUsers", "Arl_Id");
            CreateIndex("dbo.AspNetUsers", "Afp_Id");
            CreateIndex("dbo.AspNetUsers", "Eps_Id");
            CreateIndex("dbo.AspNetUsers", "Tvin_Id");
            CreateIndex("dbo.AspNetUsers", "Jemp_Id");
            CreateIndex("dbo.AspNetUsers", "Gene_Id");
            CreateIndex("dbo.AspNetUsers", "Cate_Id");
            CreateIndex("dbo.AspNetUsers", "Aemp_Id");
            CreateIndex("dbo.AspNetUsers", "Cemp_Id");
            CreateIndex("dbo.AspNetUsers", "Ciud_Id");
            CreateIndex("dbo.AspNetUsers", "Sciu_Id");
            CreateIndex("dbo.AspNetUsers", "Tdoc_Id");
        }
    }
}
