namespace PetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgePropertyAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "age");
        }
    }
}
