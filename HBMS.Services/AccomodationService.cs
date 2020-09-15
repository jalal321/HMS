using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using HMS.Data;
using HMS.Entities;
using HMS;

namespace HMS.Services
{
   public class AccomodationService
    {
        public IEnumerable<Accomodation> GetAllAccomodations()
        {
            var context = new HMSContext();
            return context.Accomodations.AsEnumerable();
        }
        public IEnumerable<Accomodation> SearchAccomodations(string searchTerm, int? typeid , int? packageid)
        {

            var context = new HMSContext();
            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (typeid.HasValue && typeid.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackage.AccomodationTypeId == typeid.Value);
            }
            
            if (packageid.HasValue && packageid.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackage.Id == packageid.Value);
            }

            return accomodations.ToList();
        }
        public IEnumerable<Accomodation> SearchAccomodations(string searchTerm, int? accomodationPackageID)
        {

            var context = new HMSContext();
            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackage.Id == accomodationPackageID.Value);
            }

            
            return accomodations.ToList();
        }

        public IEnumerable<AccomodationPackage> SearchAccomodationsAvailability(int? accomodationTypeID)
        {

            var context = new HMSContext();
            var accomodations = context.Accomodations.AsQueryable();


           List<AccomodationPackage> accomodationPackages = null;

            if (accomodationTypeID.HasValue && accomodationTypeID.Value > 0)
            {
                accomodationPackages = accomodations.Where(a => a.AccomodationPackage.AccomodationType.Id == accomodationTypeID.Value)
                    .Select(a => a.AccomodationPackage).Distinct().ToList(); 
                
            }


            return accomodationPackages;
        }

        public IEnumerable<Accomodation> CheckAccomodationsAvailability(int? accomodationPackageId, DateTime checkIn, DateTime checkOut , int? accomodationTypeId = 0)
        {

            var context = new HMSContext();
            List<Accomodation> availableAccomodations;
            if (accomodationPackageId != null)
            {
                 var allAccomodations = context.Accomodations.Where(a => a.AccomodationPackageId == accomodationPackageId && a.InProcess != true).ToList();


            var allBookings =
                context.BookingDetails
                .Where(a => a.Accomodation.AccomodationPackage.Id == accomodationPackageId)
               .ToList();


             availableAccomodations =
                allAccomodations.Where(
                    a => allBookings.Where(c=>c.AccomodationId == a.Id).All(b =>  b.Booking.FromDate >= checkOut ||   b.Booking.ToDate <= checkIn)).ToList();

            }

            else
            {
                if (accomodationTypeId != 0)
                {
                     var allAccomodations = context.Accomodations.Where(a => a.AccomodationPackage.AccomodationType.Id == accomodationTypeId && a.InProcess != true);

                var allBookings =
                    context.BookingDetails
                    .Where(a => a.Booking.Status != "CheckedOut" && a.Booking.Status != "Cancel" &&  a.Accomodation.AccomodationPackage.AccomodationType.Id == accomodationTypeId);


                 availableAccomodations =
                    allAccomodations.Where(
                        a => allBookings.Where(c => c.AccomodationId == a.Id).All(b => b.Booking.FromDate >= checkOut || b.Booking.ToDate <= checkIn)).ToList();
           
                }
                else
                {
                     var allAccomodations = context.Accomodations.Where(a => a.InProcess != true);


                var allBookings =
                    context.BookingDetails
                    .Where(a => a.Booking.Status != "CheckedOut" && a.Booking.Status != "Cancel");


                 availableAccomodations =
                    allAccomodations.Where(
                        a => allBookings.Where(c => c.AccomodationId == a.Id).All(b => b.Booking.FromDate >= checkOut || b.Booking.ToDate <= checkIn)).ToList();
          
                }

                 }

          
          
            return availableAccomodations;
        }  
       
       
       public int CountAccomodationByPackage(int? accomodationPackageID)
        {

            var context = new HMSContext();
            var packageCount = context.Accomodations.Count(a => a.AccomodationPackageId == accomodationPackageID);

            return packageCount;
        }
        public Accomodation GetAccomodationById(int? id)
        {
            var context = new HMSContext();
            return context.Accomodations.Find(id);
        }

        public bool SaveAccomodations(Accomodation accomodation)
        {
            var context = new HMSContext();
            context.Accomodations.Add(accomodation);
            var result = context.SaveChanges() > 0;

            return result;
        }
        public bool UpdateAccomodations(Accomodation accomodation)
        {
            var context = new HMSContext();
            var obj = context.Accomodations.Find(accomodation.Id);

            obj.Name = accomodation.Name;
            obj.AccomodationPackageId = accomodation.AccomodationPackageId;

            obj.Description = accomodation.Description;


            context.Entry(obj).State = EntityState.Modified;

            var result = context.SaveChanges() > 0;

            return result;
        }

        public bool DeleteAccomodations(int id)
        {
            var context = new HMSContext();
            var obj = context.Accomodations.Find(id);

            context.Entry(obj).State = EntityState.Deleted;
            var result = context.SaveChanges() > 0;

            return result;
        }
    }

}
