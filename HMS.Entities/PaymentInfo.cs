using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
  public  class PaymentInfo
    {
      public int Id { get; set; }

      public string PaymentType { get; set; }

      public string PaymentStatus { get; set; }

      public int BookingId { get; set; }

      [Required]
      public  Booking Booking { get; set; }

      
    }
}
