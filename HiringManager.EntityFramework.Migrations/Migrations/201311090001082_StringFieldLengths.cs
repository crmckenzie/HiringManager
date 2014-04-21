namespace HiringManager.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class StringFieldLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Candidates", "Name", c => c.String(maxLength: 250));
            AlterColumn("dbo.CandidateStatus", "Status", c => c.String(maxLength: 50));
            AlterColumn("dbo.Positions", "Status", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Positions", "Title", c => c.String(maxLength: 250));
            AlterColumn("dbo.Managers", "Name", c => c.String(maxLength: 250));
            AlterColumn("dbo.Managers", "Title", c => c.String(maxLength: 250));
            AlterColumn("dbo.Managers", "UserName", c => c.String(maxLength: 250));
            AlterColumn("dbo.ContactInfoes", "Type", c => c.String(maxLength: 50));
            AlterColumn("dbo.ContactInfoes", "Value", c => c.String(maxLength: 50));
            AlterColumn("dbo.Messages", "Subject", c => c.String(maxLength: 250));
            AlterColumn("dbo.Documents", "FileName", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "FileName", c => c.String());
            AlterColumn("dbo.Messages", "Subject", c => c.String());
            AlterColumn("dbo.ContactInfoes", "Value", c => c.String());
            AlterColumn("dbo.ContactInfoes", "Type", c => c.String());
            AlterColumn("dbo.Managers", "UserName", c => c.String());
            AlterColumn("dbo.Managers", "Title", c => c.String());
            AlterColumn("dbo.Managers", "Name", c => c.String());
            AlterColumn("dbo.Positions", "Title", c => c.String());
            AlterColumn("dbo.Positions", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.CandidateStatus", "Status", c => c.String());
            AlterColumn("dbo.Candidates", "Name", c => c.String());
        }
    }
}
