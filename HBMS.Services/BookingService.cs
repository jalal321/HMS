using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Data;
using HMS;
using HMS.Entities;

namespace HMS.Services
{
    public class BookingService
    {

        private static object Lock = new object();

        public string CreateBooking(Booking booking, int accomodationPackageId)
        {
            // New implementation
            lock (Lock)
            {
                AccomodationService accomodationService = new AccomodationService();
                var model =
                    accomodationService.CheckAccomodationsAvailability(
                        accomodationPackageId, booking.FromDate, booking.ToDate);

                if (model != null && model.Count() >= booking.NoOfAccomodation)
                {
                    BookingDetail bookingDetailSingleObj = new BookingDetail();

                    //if (booking.NoOfAccomodation == 1)
                    //{

                    //    bookingDetailSingleObj.AccomodationId = model.FirstOrDefault().Id;
                    //        //BookingId = bookingViewModel.
                        

                    //    //booking.BookingDetails.Add(bookingDetailSingleObj);
                    //}

                    //else
                    //{
                    // result.Data = new { success = true, message = "Rooms Availabale", data = ids };
                    List<BookingDetail> bookingDetails = new List<BookingDetail>();

                    foreach (var accomodation in model.Take(booking.NoOfAccomodation))
                    {
                        BookingDetail bookingDetailObj = new BookingDetail()
                        {
                            AccomodationId = accomodation.Id,
                            //BookingId = bookingViewModel.
                        };

                        bookingDetails.Add(bookingDetailObj);
                    }
                    //if (booking.NoOfAccomodation == 1)
                    //{
                    //    booking.BookingDetails = bookingDetails.Take(1).ToList();
                    //}
                    
                        booking.BookingDetails = bookingDetails;

                    //}



                    HMSContext context = new HMSContext();

                    context.Bookings.Add(booking);
                    //context.BookingDetails.Add(bookingDetail);
                    //var result = await context.SaveChangesAsync() > 0;
                    var result = context.SaveChanges() > 0;

                    if (result == true)
                    {
                        return
                            "You have Successfuly reserved your Accomodation , soon you will receive message and email , Booking Id :" +
                            booking.Id;
                    }

                    return "Somethinbg went wrong";

                }
                else if (model.Count() < booking.NoOfAccomodation && model.Count() != 0)
                {


                    return "You Asked for " + booking.NoOfAccomodation +
                           " Accomodations but " + model.Count() + " Accomodation Availabale Only , plz Try Again";

                }
                else
                {

                    return "All Rooms Are Booked!";
                }
            }
            // old implementation

            //HMSContext context = new HMSContext();

            //context.Bookings.Add(booking);
            ////context.BookingDetails.Add(bookingDetail);
            ////var result = await context.SaveChangesAsync() > 0;
            //var result = context.SaveChanges() > 0;
            //UpdateState(booking.BookingDetails.Select(z => z.Accomodation.Id).ToList(), false);

            //if (result == true)
            //{
            //    return booking.Id;
            //}

            ////}

            //return 0;
        }
        public void UpdateState(List<int> ids, bool status)
        {

            HMSContext context = new HMSContext();
            if (status == true)
            {
                foreach (var t in ids)
                {
                    var obj = context.Accomodations.Find(t);
                    obj.InProcess = true;
                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            else
            {
                foreach (var t in ids)
                {
                    var obj = context.Accomodations.Find(t);
                    obj.InProcess = false;
                    context.Entry(obj).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }


        }

        /// <summary>
        /// Admin Actions in Booking Service
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>

        public IEnumerable<Booking> GetAllBookings()
        {
            HMSContext context = new HMSContext();
            var model = context.Bookings.ToList();
            return model;
        }
        public IEnumerable<Booking> SearchBookings(string searchTerm, int? accomodationPackageid, string status)
        {

            var context = new HMSContext();
            var bookings = context.Bookings.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bookings = bookings.Where(a => a.GuestName.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationPackageid.HasValue && accomodationPackageid.Value > 0)
            {
                bookings = bookings.Where(a => a.BookingDetails.Any(b => b.Accomodation.AccomodationPackage.Id == accomodationPackageid.Value));
            }

            if (!string.IsNullOrEmpty(status) && status != "0")
            {
                bookings = bookings.Where(a => a.Status == status);
            }
            return bookings.ToList();
        }

        public Booking GetBookingById(int? id)
        {
            var context = new HMSContext();
            return context.Bookings.Find(id);
        }

        public bool SaveAccomodationPackages(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();
            context.AccomodationPackages.Add(accomodationPackage);
            var result = context.SaveChanges() > 0;

            return result;
        }
        public bool UpdateBooking(Booking booking)
        {
            var context = new HMSContext();
            //var obj = context.AccomodationPackages.Find(accomodationPackage.Id);
            //context.AccomodationPackagePictures.RemoveRange(obj.AccomodationPackagePictures);
            //context.AccomodationPackagePictures.AddRange(accomodationPackage.AccomodationPackagePictures);



            //obj.Name = accomodationPackage.Name;
            //obj.NoOfRoom = accomodationPackage.NoOfRoom;
            //obj.FeePerNight = accomodationPackage.FeePerNight;
            //obj.AccomodationTypeId = accomodationPackage.AccomodationTypeId;

            //obj.AccomodationPackagePictures = accomodationPackage.AccomodationPackagePictures;


            context.Entry(booking).State = EntityState.Modified;

            var result = context.SaveChanges() > 0;

            return result;
        }

        public bool DeleteAccomodationPackages(int id)
        {
            var context = new HMSContext();
            var obj = context.AccomodationPackages.Find(id);

            context.Entry(obj).State = EntityState.Deleted;
            var result = context.SaveChanges() > 0;

            return result;
        }





        /// <summary>
        /// to add entry in booking addothercharges table
        /// </summary>
        /// <param name="bookingAdditionalFee"></param>
        /// <returns></returns>
        public bool AddOtherCharges(BookingAdditionalFee bookingAdditionalFee)
        {
            var context = new HMSContext();
            context.BookingAdditionalFees.Add(bookingAdditionalFee);
            var result = context.SaveChanges() > 0;
            return result;
        }

        public bool UpdateAddionalChargesStatus(int id, int status)
        {
            HMSContext context = new HMSContext();

            var obj = context.BookingAdditionalFees.Find(id);
            obj.Status = status;
            context.Entry(obj).State = EntityState.Modified;
            var result = context.SaveChanges() > 0;
            return result;
        }
    }
}
