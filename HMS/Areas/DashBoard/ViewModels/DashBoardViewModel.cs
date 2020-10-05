using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.Areas.DashBoard.ViewModels
{
    public class DashBoardViewModel
    {
        public string Role { get; set; }

        public List<Booking> ArrivalExpetcedToday { get; set; }
        public List<Booking> DepartureExpectedToday { get; set; }

        public int TotalAccomodationPackages { get; set; }
        public int TotalAccomodations { get; set; }
        public int TotalReserved { get; set; }
        public int TotalCheckedIn { get; set; }

        public IEnumerable<Booking> CompleteBookingDetail { get; set; }
    }
}