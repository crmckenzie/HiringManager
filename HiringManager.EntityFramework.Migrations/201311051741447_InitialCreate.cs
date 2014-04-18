namespace HiringManager.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CandidateId);
            
            CreateTable(
                "dbo.CandidateStatus",
                c => new
                    {
                        CandidateStatusId = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(),
                        PositionId = c.Int(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.CandidateStatusId)
                .ForeignKey("dbo.Candidates", t => t.CandidateId)
                .ForeignKey("dbo.Positions", t => t.PositionId)
                .Index(t => t.CandidateId)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        PositionId = c.Int(nullable: false, identity: true),
                        FilledById = c.Int(),
                        CreatedById = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                        Title = c.String(),
                        OpenDate = c.DateTime(),
                        FilledDate = c.DateTime(),
                        CreatedBy_ManagerId = c.Int(),
                        FilledBy_CandidateId = c.Int(),
                    })
                .PrimaryKey(t => t.PositionId)
                .ForeignKey("dbo.Managers", t => t.CreatedBy_ManagerId)
                .ForeignKey("dbo.Candidates", t => t.FilledBy_CandidateId)
                .Index(t => t.CreatedBy_ManagerId)
                .Index(t => t.FilledBy_CandidateId);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        ManagerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Title = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.ManagerId);
            
            CreateTable(
                "dbo.ContactInfoes",
                c => new
                    {
                        ContactInfoId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Value = c.String(),
                        Candidate_CandidateId = c.Int(),
                        Manager_ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.ContactInfoId)
                .ForeignKey("dbo.Candidates", t => t.Candidate_CandidateId)
                .ForeignKey("dbo.Managers", t => t.Manager_ManagerId)
                .Index(t => t.Candidate_CandidateId)
                .Index(t => t.Manager_ManagerId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                        Candidate_CandidateId = c.Int(),
                        Manager_ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Candidates", t => t.Candidate_CandidateId)
                .ForeignKey("dbo.Managers", t => t.Manager_ManagerId)
                .Index(t => t.Candidate_CandidateId)
                .Index(t => t.Manager_ManagerId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        StorageId = c.String(),
                        Message_MessageId = c.Int(),
                        Candidate_CandidateId = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentId)
                .ForeignKey("dbo.Messages", t => t.Message_MessageId)
                .ForeignKey("dbo.Candidates", t => t.Candidate_CandidateId)
                .Index(t => t.Message_MessageId)
                .Index(t => t.Candidate_CandidateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "Candidate_CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Positions", "FilledBy_CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Positions", "CreatedBy_ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Messages", "Manager_ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Messages", "Candidate_CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Documents", "Message_MessageId", "dbo.Messages");
            DropForeignKey("dbo.ContactInfoes", "Manager_ManagerId", "dbo.Managers");
            DropForeignKey("dbo.ContactInfoes", "Candidate_CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.CandidateStatus", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.CandidateStatus", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.Documents", new[] { "Candidate_CandidateId" });
            DropIndex("dbo.Positions", new[] { "FilledBy_CandidateId" });
            DropIndex("dbo.Positions", new[] { "CreatedBy_ManagerId" });
            DropIndex("dbo.Messages", new[] { "Manager_ManagerId" });
            DropIndex("dbo.Messages", new[] { "Candidate_CandidateId" });
            DropIndex("dbo.Documents", new[] { "Message_MessageId" });
            DropIndex("dbo.ContactInfoes", new[] { "Manager_ManagerId" });
            DropIndex("dbo.ContactInfoes", new[] { "Candidate_CandidateId" });
            DropIndex("dbo.CandidateStatus", new[] { "PositionId" });
            DropIndex("dbo.CandidateStatus", new[] { "CandidateId" });
            DropTable("dbo.Documents");
            DropTable("dbo.Messages");
            DropTable("dbo.ContactInfoes");
            DropTable("dbo.Managers");
            DropTable("dbo.Positions");
            DropTable("dbo.CandidateStatus");
            DropTable("dbo.Candidates");
        }
    }
}
