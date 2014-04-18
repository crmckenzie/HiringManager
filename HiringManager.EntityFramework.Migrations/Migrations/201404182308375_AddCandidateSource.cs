namespace HiringManager.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCandidateSource : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        SourceId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.SourceId)
                .Index(t => t.Name, unique: true);
            
            AddColumn("dbo.Candidates", "SourceId", c => c.Int());
            CreateIndex("dbo.Candidates", "SourceId");
            AddForeignKey("dbo.Candidates", "SourceId", "dbo.Sources", "SourceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candidates", "SourceId", "dbo.Sources");
            DropIndex("dbo.Sources", new[] { "Name" });
            DropIndex("dbo.Candidates", new[] { "SourceId" });
            DropColumn("dbo.Candidates", "SourceId");
            DropTable("dbo.Sources");
        }
    }
}
