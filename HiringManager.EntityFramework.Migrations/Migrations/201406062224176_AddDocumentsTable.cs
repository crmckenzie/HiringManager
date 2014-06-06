namespace HiringManager.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocumentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        FileName = c.String(),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Candidates", t => t.CandidateId, cascadeDelete: true)
                .Index(t => t.CandidateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Documents", new[] { "CandidateId" });
            DropTable("dbo.Documents");
        }
    }
}
