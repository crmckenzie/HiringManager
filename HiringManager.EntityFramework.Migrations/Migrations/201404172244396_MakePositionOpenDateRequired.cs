namespace HiringManager.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class MakePositionOpenDateRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Positions", "OpenDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Positions", "FilledById");
            CreateIndex("dbo.ContactInfoes", "CandidateId");
            CreateIndex("dbo.ContactInfoes", "ManagerId");
        }

        public override void Down()
        {
            DropIndex("dbo.ContactInfoes", new[] { "ManagerId" });
            DropIndex("dbo.ContactInfoes", new[] { "CandidateId" });
            DropIndex("dbo.Positions", new[] { "FilledById" });
            AlterColumn("dbo.Positions", "OpenDate", c => c.DateTime());
        }
    }
}
