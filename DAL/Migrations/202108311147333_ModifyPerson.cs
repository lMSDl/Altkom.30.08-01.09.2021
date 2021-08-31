namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyPerson : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "FirstName", c => c.String(maxLength: 15));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "BirthDate", c => c.DateTime());
            AlterColumn("dbo.People", "Gender", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.People", "BirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.People", "LastName", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String());
        }
    }
}
