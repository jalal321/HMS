using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Data;
using HMS.Entities;

namespace HMS.Services
{
    public class DashBoardService
    {
        public bool SavePictureList(List<Picture> pictures)
        {
            HMSContext db = new HMSContext();
            foreach (var pic in pictures)
            {
                db.Pictures.Add(pic);
            }


            return db.SaveChanges() > 0;
        }

        public bool SavePicture(Picture pictures)
        {
            HMSContext db = new HMSContext();

            db.Pictures.Add(pictures);

            return db.SaveChanges() > 0;
        }


        //methods used In dashboard controller

        public int AccomodationPackagesCount()
        {
            HMSContext db = new HMSContext();


            return db.AccomodationPackages.Count();
        }
        public int AccomodationsCount()
        {
            HMSContext db = new HMSContext();


            return db.Accomodations.Count();
        }

        public int TotalAccomodationsReserved()
        {
            HMSContext db = new HMSContext();


            return db.Bookings.Count(a => a.Status == "Reserved");
        }
        public int TotalAccomodationsCheckedIn()
        {
            HMSContext db = new HMSContext();


            return db.Bookings.Count(a => a.Status == "CheckedIn");
        }

        public IEnumerable<Booking> ComepleteBookingDetail()
        {
            HMSContext db = new HMSContext();


            return db.Bookings.ToList();
        }

        public List<Booking> ArrivalsExpetced()
        {
            HMSContext db = new HMSContext();

            var date = DateTime.Now.Date;
            return db.Bookings.Where(a => a.FromDate == date).ToList();
        }

        public List<Booking> DeparturesExpected()
        {
            HMSContext db = new HMSContext();

            var date = DateTime.Now.Date;
            return db.Bookings.Where(a => a.ToDate == date).ToList();
        }
        
        public List<Message> GetMessages()
        {
            HMSContext db = new HMSContext();

            
            return db.Messages.ToList();
        }



        public List<Booking> GetBookingsReserved()
        {
            HMSContext db = new HMSContext();
            return db.Bookings.Where(a => a.Status == "Reserved").ToList();   
        }
    }
}
