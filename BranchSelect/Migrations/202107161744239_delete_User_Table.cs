namespace BranchSelect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_User_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.StudentBranches", "StudentId", "dbo.Students");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.StudentBranches", new[] { "StudentId" });
            DropPrimaryKey("dbo.StudentBranches");
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.StudentBranches", "StudentId", c => c.String(nullable: false, maxLength: 11, unicode: false));
            AlterColumn("dbo.Students", "Id", c => c.String(nullable: false, maxLength: 11, unicode: false));
            AddPrimaryKey("dbo.StudentBranches", "StudentId");
            AddPrimaryKey("dbo.Students", "Id");
            CreateIndex("dbo.StudentBranches", "StudentId");
            AddForeignKey("dbo.StudentBranches", "StudentId", "dbo.Students", "Id");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Passsword = c.String(),
                        RoleId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.StudentBranches", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentBranches", new[] { "StudentId" });
            DropPrimaryKey("dbo.Students");
            DropPrimaryKey("dbo.StudentBranches");
            AlterColumn("dbo.Students", "Id", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.StudentBranches", "StudentId", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Students", "Id");
            AddPrimaryKey("dbo.StudentBranches", "StudentId");
            CreateIndex("dbo.StudentBranches", "StudentId");
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.StudentBranches", "StudentId", "dbo.Students", "Id");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
