namespace HiringManager.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOpenings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Positions", "FilledById", "dbo.Candidates");
            DropForeignKey("dbo.Positions", "FK_dbo.Positions_dbo.Candidates_FilledBy_CandidateId");
            DropIndex("dbo.Positions", new[] { "FilledById" });
            DropIndex("dbo.Positions", "IX_FilledBy_CandidateId");

            CreateTable(
                "dbo.Openings",
                c => new
                    {
                        OpeningId = c.Int(nullable: false, identity: true),
                        PositionId = c.Int(nullable: false),
                        FilledById = c.Int(),
                        FilledDate = c.DateTime(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.OpeningId)
                .ForeignKey("dbo.Candidates", t => t.FilledById)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .Index(t => new { t.PositionId, t.FilledById }, unique: true, name: "UQ_Opening");
            
            DropColumn("dbo.Positions", "FilledById");
            DropColumn("dbo.Positions", "FilledDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "FilledDate", c => c.DateTime());
            AddColumn("dbo.Positions", "FilledById", c => c.Int());
            DropForeignKey("dbo.Openings", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.Openings", "FilledById", "dbo.Candidates");
            DropIndex("dbo.Openings", "UQ_Opening");
            DropTable("dbo.Openings");
            CreateIndex("dbo.Positions", "FilledById");
            AddForeignKey("dbo.Positions", "FilledById", "dbo.Candidates", "CandidateId");
        }
    }
}
