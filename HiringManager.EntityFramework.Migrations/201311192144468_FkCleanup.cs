namespace HiringManager.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FkCleanup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "Message_MessageId", "dbo.Messages");
            DropForeignKey("dbo.Messages", "Candidate_CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.Messages", "Manager_ManagerId", "dbo.Managers");
            DropForeignKey("dbo.Documents", "Candidate_CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.CandidateStatus", "CandidateId", "dbo.Candidates");
            DropForeignKey("dbo.CandidateStatus", "PositionId", "dbo.Positions");
            DropIndex("dbo.Documents", new[] { "Message_MessageId" });
            DropIndex("dbo.Messages", new[] { "Candidate_CandidateId" });
            DropIndex("dbo.Messages", new[] { "Manager_ManagerId" });
            DropIndex("dbo.Documents", new[] { "Candidate_CandidateId" });
            DropIndex("dbo.CandidateStatus", new[] { "CandidateId" });
            DropIndex("dbo.CandidateStatus", new[] { "PositionId" });
            DropColumn("dbo.Positions", "FilledById");
            RenameColumn(table: "dbo.ContactInfoes", name: "Candidate_CandidateId", newName: "CandidateId");
            RenameColumn(table: "dbo.ContactInfoes", name: "Manager_ManagerId", newName: "ManagerId");
            RenameColumn(table: "dbo.Positions", name: "FilledBy_CandidateId", newName: "FilledById");
            AlterColumn("dbo.CandidateStatus", "CandidateId", c => c.Int(nullable: false));
            AlterColumn("dbo.CandidateStatus", "PositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.CandidateStatus", "CandidateId");
            CreateIndex("dbo.CandidateStatus", "PositionId");
            AddForeignKey("dbo.CandidateStatus", "CandidateId", "dbo.Candidates", "CandidateId", cascadeDelete: true);
            AddForeignKey("dbo.CandidateStatus", "PositionId", "dbo.Positions", "PositionId", cascadeDelete: true);
            DropTable("dbo.Messages");
            DropTable("dbo.Documents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 1000),
                        StorageId = c.String(),
                        Message_MessageId = c.Int(),
                        Candidate_CandidateId = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 250),
                        Body = c.String(),
                        Candidate_CandidateId = c.Int(),
                        Manager_ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.MessageId);
            
            DropForeignKey("dbo.CandidateStatus", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.CandidateStatus", "CandidateId", "dbo.Candidates");
            DropIndex("dbo.CandidateStatus", new[] { "PositionId" });
            DropIndex("dbo.CandidateStatus", new[] { "CandidateId" });
            AlterColumn("dbo.CandidateStatus", "PositionId", c => c.Int());
            AlterColumn("dbo.CandidateStatus", "CandidateId", c => c.Int());
            RenameColumn(table: "dbo.Positions", name: "FilledById", newName: "FilledBy_CandidateId");
            RenameColumn(table: "dbo.ContactInfoes", name: "ManagerId", newName: "Manager_ManagerId");
            RenameColumn(table: "dbo.ContactInfoes", name: "CandidateId", newName: "Candidate_CandidateId");
            AddColumn("dbo.Positions", "FilledById", c => c.Int());
            CreateIndex("dbo.CandidateStatus", "PositionId");
            CreateIndex("dbo.CandidateStatus", "CandidateId");
            CreateIndex("dbo.Documents", "Candidate_CandidateId");
            CreateIndex("dbo.Messages", "Manager_ManagerId");
            CreateIndex("dbo.Messages", "Candidate_CandidateId");
            CreateIndex("dbo.Documents", "Message_MessageId");
            AddForeignKey("dbo.CandidateStatus", "PositionId", "dbo.Positions", "PositionId");
            AddForeignKey("dbo.CandidateStatus", "CandidateId", "dbo.Candidates", "CandidateId");
            AddForeignKey("dbo.Documents", "Candidate_CandidateId", "dbo.Candidates", "CandidateId");
            AddForeignKey("dbo.Messages", "Manager_ManagerId", "dbo.Managers", "ManagerId");
            AddForeignKey("dbo.Messages", "Candidate_CandidateId", "dbo.Candidates", "CandidateId");
            AddForeignKey("dbo.Documents", "Message_MessageId", "dbo.Messages", "MessageId");
        }
    }
}
