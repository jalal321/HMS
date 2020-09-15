namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstatusfieldinbooking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        PaymentType = c.String(),
                        PaymentStatus = c.String(),
                        BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Bookings", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Bookings", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentInfoes", "Id", "dbo.Bookings");
            DropIndex("dbo.PaymentInfoes", new[] { "Id" });
            DropColumn("dbo.Bookings", "Status");
            DropColumn("dbo.Bookings", "TotalAmount");
            DropTable("dbo.PaymentInfoes");
        }
    }
}
