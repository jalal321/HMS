namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingdetailbookingadditionalfeeincludedextrafields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "NoOfAccomodation", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "SpecialNote", c => c.String());
            AddColumn("dbo.Bookings", "Accomodation_Id", c => c.Int());
            CreateIndex("dbo.Bookings", "Accomodation_Id");
            AddForeignKey("dbo.Bookings", "Accomodation_Id", "dbo.Accomodations", "Id");
            DropColumn("dbo.Bookings", "NoOfRooms");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "NoOfRooms", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bookings", "Accomodation_Id", "dbo.Accomodations");
            DropIndex("dbo.Bookings", new[] { "Accomodation_Id" });
            DropColumn("dbo.Bookings", "Accomodation_Id");
            DropColumn("dbo.Bookings", "SpecialNote");
            DropColumn("dbo.Bookings", "NoOfAccomodation");
        }
    }
}
