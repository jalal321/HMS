using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HMS.Code
{
    public class Enums
    {
        public enum BookingStatus
        {
            [Description("Client has reserved but not checked in yet")]

            Reserved = 1,
            [Description("Client has checked-In")]

            CheckedIn = 2,
            [Description("Client has checked-Out")]
            
            CheckedOut = 3,
            [Description("Booking has been Canceled")]

            Cancel = 4
        }
    }
}