namespace HiringManager.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class PositionCreatedByRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Positions", "CreatedBy_ManagerId", "dbo.Managers");
            DropIndex("dbo.Positions", new[] { "CreatedBy_ManagerId" });
            CreateIndex("dbo.Positions", "CreatedById");
            AddForeignKey("dbo.Positions", "CreatedById", "dbo.Managers", "ManagerId", cascadeDelete: true);
            DropColumn("dbo.Positions", "CreatedBy_ManagerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "CreatedBy_ManagerId", c => c.Int());
            DropForeignKey("dbo.Positions", "CreatedById", "dbo.Managers");
            DropIndex("dbo.Positions", new[] { "CreatedById" });
            CreateIndex("dbo.Positions", "CreatedBy_ManagerId");
            AddForeignKey("dbo.Positions", "CreatedBy_ManagerId", "dbo.Managers", "ManagerId");
        }
    }
}
