namespace TestIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAgeField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonalInformations", "Age", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonalInformations", "Age", c => c.Int(nullable: false));
        }
    }
}
