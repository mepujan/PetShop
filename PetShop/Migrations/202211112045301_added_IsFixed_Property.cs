namespace PetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_IsFixed_Property : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "IsFixed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pets", "IsFixed");
        }
    }
}
