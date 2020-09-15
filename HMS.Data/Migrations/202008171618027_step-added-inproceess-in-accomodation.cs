namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stepaddedinproceessinaccomodation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accomodations", "InProcess", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accomodations", "InProcess");
        }
    }
}
