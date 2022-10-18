namespace PetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingAgeToPetModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "Age");
        }
    }
}
