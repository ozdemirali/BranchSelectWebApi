namespace BranchSelect.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Student_Model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Address", c => c.String());
            DropColumn("dbo.Students", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Adress", c => c.String());
            DropColumn("dbo.Students", "Address");
        }
    }
}
