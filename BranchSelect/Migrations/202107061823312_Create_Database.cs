namespace BranchSelect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentBranches",
                c => new
                    {
                        StudentIdentity = c.String(nullable: false, maxLength: 50, unicode: false),
                        FirstSelect = c.Byte(nullable: false),
                        SecondSelect = c.Byte(nullable: false),
                        Confirmation = c.Boolean(nullable: false),
                        Branch_Id = c.Byte(),
                    })
                .PrimaryKey(t => t.StudentIdentity)
                .ForeignKey("dbo.Students", t => t.StudentIdentity)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .Index(t => t.StudentIdentity)
                .Index(t => t.Branch_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Identity = c.String(nullable: false, maxLength: 50, unicode: false),
                        NameAndSurname = c.String(),
                        ParentNameAndSurname = c.String(),
                        Class = c.String(),
                        Adress = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Score = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Identity);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                        BranchTeacher = c.String(),
                        AssistantDirector = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentBranches", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.StudentBranches", "StudentIdentity", "dbo.Students");
            DropIndex("dbo.StudentBranches", new[] { "Branch_Id" });
            DropIndex("dbo.StudentBranches", new[] { "StudentIdentity" });
            DropTable("dbo.Users");
            DropTable("dbo.Schools");
            DropTable("dbo.Students");
            DropTable("dbo.StudentBranches");
            DropTable("dbo.Branches");
        }
    }
}
