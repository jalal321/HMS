using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        public string GuestName { get; set; }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        /// <summary>
        /// no of stay nights
        /// </summary>
        public int Duration { get; set; }
        public bool BreakFast { get; set; }
        public int Adult { get; set; }
        public int Children { get; set; }
        public int NoOfAccomodation { get; set; }

        [EmailAddress]
        public string Email{ get; set; }

        [Phone]
        public string Phone{ get; set; }
        
        public string Address{ get; set; }
        public string SpecialNote{ get; set; }

        public virtual List<BookingDetail> BookingDetails { get; set; }
        public List<BookingAdditionalFee> BookingAdditionalFees { get; set; }

    }
}
