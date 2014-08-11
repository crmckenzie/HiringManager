namespace HiringManager.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddNotesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        CandidateStatusId = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Authored = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.Managers", t => t.AuthorId, cascadeDelete: false)
                .ForeignKey("dbo.CandidateStatus", t => t.CandidateStatusId, cascadeDelete: false)
                .Index(t => t.CandidateStatusId)
                .Index(t => t.AuthorId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Notes", "CandidateStatusId", "dbo.CandidateStatus");
            DropForeignKey("dbo.Notes", "AuthorId", "dbo.Managers");
            DropIndex("dbo.Notes", new[] { "AuthorId" });
            DropIndex("dbo.Notes", new[] { "CandidateStatusId" });
            DropTable("dbo.Notes");
        }
    }
}
