namespace TestIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeAgeField2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonalInformations", "RegistrationDate", c => c.DateTime());
            AlterColumn("dbo.PersonalInformations", "StudynDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonalInformations", "StudynDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PersonalInformations", "RegistrationDate", c => c.DateTime(nullable: false));
        }
    }
}
