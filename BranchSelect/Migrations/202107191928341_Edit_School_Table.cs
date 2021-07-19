namespace BranchSelect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_School_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schools", "MinClassCount", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schools", "MinClassCount");
        }
    }
}
