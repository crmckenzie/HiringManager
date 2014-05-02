namespace HiringManager.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueConstraintToCandidateStatus : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CandidateStatus", new[] { "CandidateId" });
            DropIndex("dbo.CandidateStatus", new[] { "PositionId" });
            CreateIndex("dbo.CandidateStatus", new[] { "PositionId", "CandidateId" }, unique: true, name: "UQ_CandidateStatus");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CandidateStatus", "UQ_CandidateStatus");
            CreateIndex("dbo.CandidateStatus", "PositionId");
            CreateIndex("dbo.CandidateStatus", "CandidateId");
        }
    }
}
