namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingdetailbookingadditionalfeeincluded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "AccomodationId", "dbo.Accomodations");
            DropIndex("dbo.Bookings", new[] { "AccomodationId" });
            CreateTable(
                "dbo.BookingAdditionalFees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.BookingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccomodationId = c.Int(nullable: false),
                        BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: true)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .Index(t => t.AccomodationId)
                .Index(t => t.BookingId);
            
            AddColumn("dbo.AccomodationPackages", "Description", c => c.String());
            AddColumn("dbo.Bookings", "GuestName", c => c.String());
            AddColumn("dbo.Bookings", "ToDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "BreakFast", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bookings", "Adult", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "Children", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "NoOfRooms", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "Email", c => c.String());
            AddColumn("dbo.Bookings", "Phone", c => c.String());
            AddColumn("dbo.Bookings", "Address", c => c.String());
            DropColumn("dbo.Bookings", "AccomodationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "AccomodationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.BookingDetails", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.BookingDetails", "AccomodationId", "dbo.Accomodations");
            DropForeignKey("dbo.BookingAdditionalFees", "BookingId", "dbo.Bookings");
            DropIndex("dbo.BookingDetails", new[] { "BookingId" });
            DropIndex("dbo.BookingDetails", new[] { "AccomodationId" });
            DropIndex("dbo.BookingAdditionalFees", new[] { "BookingId" });
            DropColumn("dbo.Bookings", "Address");
            DropColumn("dbo.Bookings", "Phone");
            DropColumn("dbo.Bookings", "Email");
            DropColumn("dbo.Bookings", "NoOfRooms");
            DropColumn("dbo.Bookings", "Children");
            DropColumn("dbo.Bookings", "Adult");
            DropColumn("dbo.Bookings", "BreakFast");
            DropColumn("dbo.Bookings", "ToDate");
            DropColumn("dbo.Bookings", "GuestName");
            DropColumn("dbo.AccomodationPackages", "Description");
            DropTable("dbo.BookingDetails");
            DropTable("dbo.BookingAdditionalFees");
            CreateIndex("dbo.Bookings", "AccomodationId");
            AddForeignKey("dbo.Bookings", "AccomodationId", "dbo.Accomodations", "Id", cascadeDelete: true);
        }
    }
}
