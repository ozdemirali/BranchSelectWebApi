namespace BranchSelect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_StudentBranch_Table1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentBranches", "Result", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentBranches", "Result");
        }
    }
}
