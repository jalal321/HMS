namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepaymentinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accomodations", "IsHold", c => c.Boolean(nullable: false));
            AddColumn("dbo.PaymentInfoes", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PaymentInfoes", "CardNo", c => c.String());
            AlterColumn("dbo.PaymentInfoes", "PaymentType", c => c.String(nullable: false));
            AlterColumn("dbo.PaymentInfoes", "PaymentStatus", c => c.String(nullable: false));
            DropColumn("dbo.Accomodations", "InProcess");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accomodations", "InProcess", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PaymentInfoes", "PaymentStatus", c => c.String());
            AlterColumn("dbo.PaymentInfoes", "PaymentType", c => c.String());
            DropColumn("dbo.PaymentInfoes", "CardNo");
            DropColumn("dbo.PaymentInfoes", "AmountPaid");
            DropColumn("dbo.Accomodations", "IsHold");
        }
    }
}
