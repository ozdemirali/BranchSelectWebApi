namespace BranchSelect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Student_StudentBranch_Tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Confirmation", c => c.Boolean(nullable: false));
            DropColumn("dbo.StudentBranches", "Confirmation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentBranches", "Confirmation", c => c.Boolean(nullable: false));
            DropColumn("dbo.Students", "Confirmation");
        }
    }
}
