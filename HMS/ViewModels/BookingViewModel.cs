using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.ViewModels
{
    public class BookingGetViewModel
    {
        public AccomodationType AccomodationType { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }
        public int AccomodationTypeId { get; set; }
        public int AccomodationPackageId { get; set; }

        /// <summary>
        /// AccomodationId for booking that accomodation against boking request , would be saved in AcccomodationDetail table
        /// </summary>
        public string AccomodationId { get; set; }

        //public string GuestName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int StayDuration { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int NoOfAccomodation { get; set; }

        //[Phone]
        //public string Phone { get; set; }

        //[EmailAddress]
        //public string Email { get; set; }
        public bool BreakFast { get; set; }

        public string PaymentType { get; set; }
        public string PaymentStatus { get; set; }
        //public string Address { get; set; }
        //public string SpecialNote { get; set; }
    }  
    
    public class BookingPostViewModel
    {
        public AccomodationType AccomodationType { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }
        public int AccomodationTypeId { get; set; }
        public int AccomodationPackageId { get; set; }

        /// <summary>
        /// AccomodationId for booking that accomodation against boking request , would be saved in AcccomodationDetail table
        /// </summary>
        [Required]
         
        public string AccomodationId { get; set; }
        [Required] 

        public string GuestName { get; set; }
        [Required] 

        public DateTime CheckIn { get; set; }
        [Required] 

        public DateTime CheckOut { get; set; }
        [Required] 

        public int StayDuration { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        [Required] 

        public int NoOfAccomodation { get; set; }

        [Phone]
        [Required] 

        public string Phone { get; set; }

        [EmailAddress]
        [Required] 

        public string Email { get; set; }
        public bool BreakFast { get; set; }
        [Required] 

        public string Address { get; set; }
        public string SpecialNote { get; set; }
        public string PaymentType { get; set; }
        public string PaymentStatus { get; set; }
    }
}