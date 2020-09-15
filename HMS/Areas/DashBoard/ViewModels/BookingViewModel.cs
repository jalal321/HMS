using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Code;
using HMS.Entities;
using HMS.ViewModels;

namespace HMS.Areas.DashBoard.ViewModels
{
    public class BookingListingViewModel
    {

        public IEnumerable<Booking> Bookings { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

        public int? AccomodationPackageId { get; set; }

        public Pager pager { get; set; }

        public int totalRecord { get; set; }


        public Enums.BookingStatus Status { get; set; }
    }
    
    public class BookingActionViewModel
    {

        public Booking Booking { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

        public int AccomodationPackageId { get; set; }
        public string AccomodationPackageName { get; set; }

        //booking id no
        public int? Id { get; set; } 
        
        //bookingAddional fee otamount   oc == other charges
        public int OcAmount { get; set; }
        //booking Addional fee table fee detail
        public string OcDetails { get; set; }
    }
}