namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatusinbookingaditionalfee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingAdditionalFees", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingAdditionalFees", "Status");
        }
    }
}
