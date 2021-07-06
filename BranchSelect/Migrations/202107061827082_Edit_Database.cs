namespace BranchSelect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Database : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentBranches", "StudentIdentity", "dbo.Students");
            RenameColumn(table: "dbo.StudentBranches", name: "StudentIdentity", newName: "StudentId");
            RenameIndex(table: "dbo.StudentBranches", name: "IX_StudentIdentity", newName: "IX_StudentId");
            DropPrimaryKey("dbo.Students");
            AddColumn("dbo.Students", "Id", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Students", "Id");
            AddForeignKey("dbo.StudentBranches", "StudentId", "dbo.Students", "Id");
            DropColumn("dbo.Students", "Identity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Identity", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropForeignKey("dbo.StudentBranches", "StudentId", "dbo.Students");
            DropPrimaryKey("dbo.Students");
            DropColumn("dbo.Students", "Id");
            AddPrimaryKey("dbo.Students", "Identity");
            RenameIndex(table: "dbo.StudentBranches", name: "IX_StudentId", newName: "IX_StudentIdentity");
            RenameColumn(table: "dbo.StudentBranches", name: "StudentId", newName: "StudentIdentity");
            AddForeignKey("dbo.StudentBranches", "StudentIdentity", "dbo.Students", "Identity");
        }
    }
}
