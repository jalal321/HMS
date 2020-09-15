using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
   public class BookingAdditionalFee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Fee { get; set; }

        /// <summary>
        /// to change status whether its active (1) or canceled (0) , 
        /// </summary>
        [DefaultValue(1)]
        public int Status { get; set; }

        public int BookingId { get; set; }
        public  Booking Booking { get; set; }
    }
}
