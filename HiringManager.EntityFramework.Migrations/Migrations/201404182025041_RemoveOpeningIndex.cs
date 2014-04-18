namespace HiringManager.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOpeningIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Openings", "UQ_Opening");
            CreateIndex("dbo.Openings", "PositionId");
            CreateIndex("dbo.Openings", "FilledById");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Openings", new[] { "FilledById" });
            DropIndex("dbo.Openings", new[] { "PositionId" });
            CreateIndex("dbo.Openings", new[] { "PositionId", "FilledById" }, unique: true, name: "UQ_Opening");
        }
    }
}
