namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContactModels", "Me");
        }
        
        public override void Down()
        {
        }
    }
}
