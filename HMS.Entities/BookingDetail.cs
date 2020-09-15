using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
    public class BookingDetail
    {
        public int Id { get; set; }

        public int AccomodationId { get; set; }

        public virtual Accomodation Accomodation { get; set; }
        
        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; }

    }
}
