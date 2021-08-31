namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeIntToPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "SomeInt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "SomeInt");
        }
    }
}
