namespace BranchSelect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_BranchTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentBranches", "Branch_Id", "dbo.Branches");
            DropIndex("dbo.StudentBranches", new[] { "Branch_Id" });
            DropColumn("dbo.StudentBranches", "Branch_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentBranches", "Branch_Id", c => c.Byte());
            CreateIndex("dbo.StudentBranches", "Branch_Id");
            AddForeignKey("dbo.StudentBranches", "Branch_Id", "dbo.Branches", "Id");
        }
    }
}
