using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using HMS.Code;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;

namespace HMS.Controllers
{
    public class BookingController : Controller
    {
        //private static object Lock = new object();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        AccomodationService accomodationService = new AccomodationService();
        BookingService bookingService = new BookingService();
        // GET: Booking
        public ActionResult Index(int packageId)
        {

            var model = accomodationPackagesService.GetAccomodationPackageById(packageId);
            // ccheck here wheteher room available or not
            return View(model);
        }




        [HttpGet]
        public ActionResult Action(BookingGetViewModel bookingViewModel)
        {
            //to check if Accomodation is Available
            //var avialableAccomodations = accomodationService.CheckAccomodationsAvailability(bookingViewModel.AccomodationPackageId, bookingViewModel.CheckIn, bookingViewModel.CheckOut);
            //var bdate = model.Select(a => a.Bookings.Select(b => b.FromDate));

            //if (avialableAccomodations != null && avialableAccomodations.Count() >= bookingViewModel.NoOfAccomodation)
            //{
            //var roomNos = avialableAccomodations.Select(a => a.Id).Take(bookingViewModel.NoOfAccomodation).ToList();

            //int[] ids = roomNos.ToArray();

            // to convert list<int> to array delimeted (seperated) by ","
            //var ids = string.Join(",", roomNos);

            //foreach (var room in roomNos)
            //{

            //}
            //bookingViewModel.AccomodationId = ids;
            //bookingViewModel.AccomodationId = roomsAvailable;

            //if (bookingViewModel.BreakFast)
            //{
            //    bookingViewModel.BreakFast = true;
            //}
            //ViewBag.result = true;
            //ViewBag.msg =  "Congratulations! required rooms are available.";
            //avialableAccomodations.Data = new { success = true, message = "Rooms Availabale", data = roomNos };

            //}
            //else if (avialableAccomodations != null && avialableAccomodations.Count() < bookingViewModel.NoOfAccomodation)
            //{
            //    //avialableAccomodations.Data = new
            //    //{
            //    //    success = false,
            //    //    message = "You Asked for " + bookingViewModel.NoOfAccomodation +
            //    //              " Accomodations but " + model.Count() + " Accomodation Availabale Only"
            //    //};
            //    ViewBag.result = false;
            //    ViewBag.msg = "You Asked for " + bookingViewModel.NoOfAccomodation +
            //                      " Accomodations but " + avialableAccomodations.Count() + " Accomodation Availabale Only.";

            //}
            //else
            //{
            //avialableAccomodations.Data = new { success = false, message = "Rooms not Availabale" };
            //    ViewBag.result = false;
            //    ViewBag.msg = "sorry! All rooms are Booked.";

            //}
            //

            // calculate total bill including taxes and fbreakfast charges


            bookingViewModel.AccomodationPackage =
                accomodationPackagesService.GetAccomodationPackageById(bookingViewModel.AccomodationPackageId);
            var model = bookingViewModel;


            // ccheck here wheteher room available or not
            return PartialView("_Action", model);
        }


        [HttpPost]
        [ActionName("Action")]
        public ActionResult ActionPost(BookingPostViewModel bookingViewModel)
        {
            string bookingNo = null;
            if (bookingViewModel.GuestName != null && bookingViewModel.CheckIn != null)
            {
                //if (bookingViewModel.BreakFast == 1)
                //{
                    
                //}

                bookingViewModel.AccomodationPackage =
                    accomodationPackagesService.GetAccomodationPackageById(bookingViewModel.AccomodationPackageId);

                //List<BookingDetail> bookingDetails = new List<BookingDetail>();
                //var ids = bookingViewModel.AccomodationId.Split(',').Select(id => int.Parse(id)); 

                //foreach (var id in ids)
                //{
                //    BookingDetail bookingDetailObj = new BookingDetail()
                //    {
                //        AccomodationId = id,
                //        //BookingId = bookingViewModel.
                //    };

                //    bookingDetails.Add(bookingDetailObj);
                //}


                var roomsTotal = (bookingViewModel.NoOfAccomodation * bookingViewModel.AccomodationPackage.FeePerNight) * bookingViewModel.StayDuration;
                var vatTax = (5 * roomsTotal) / 100;
                var tourismTax = 5 * (bookingViewModel.StayDuration * bookingViewModel.NoOfAccomodation);
                var breakFastTotals = 0;
                if (bookingViewModel.BreakFast == true)
                {
                    breakFastTotals = ((bookingViewModel.Adults * 7) + (bookingViewModel.Children * 3)) * bookingViewModel.StayDuration * bookingViewModel.NoOfAccomodation;

                }

                var grandTotal = roomsTotal + vatTax + tourismTax + breakFastTotals;


                Booking bookingObj = new Booking()
                {
                    GuestName = bookingViewModel.GuestName,
                    FromDate = bookingViewModel.CheckIn,
                    ToDate = bookingViewModel.CheckOut,
                    Duration = bookingViewModel.StayDuration,
                    BreakFast = bookingViewModel.BreakFast,
                    TotalAmount = grandTotal,
                    Status = Enums.BookingStatus.Reserved.ToString(),
                    Adult = bookingViewModel.Adults,
                    Children = bookingViewModel.Children,
                    NoOfAccomodation = bookingViewModel.NoOfAccomodation,
                    Email = bookingViewModel.Email,
                    Phone = bookingViewModel.Phone,
                    Address = bookingViewModel.Address,
                    SpecialNote = bookingViewModel.SpecialNote,
                    
                };
                PaymentInfo paymentobj = new PaymentInfo()
                {
                    PaymentType = bookingViewModel.PaymentType,
                    PaymentStatus = "unpaid",

                };

                bookingObj.PaymentInfo = paymentobj;

                //lock (Lock)
                //{

                bookingNo = bookingService.CreateBooking(bookingObj, bookingViewModel.AccomodationPackageId);

                //}
            }

            // ccheck here wheteher room available or not
            return PartialView("ActionPost", bookingNo);
        }

        [HttpPost]
        public JsonResult CheckAccomodationAvailibility(BookingGetViewModel bookingViewModel)
        {
            JsonResult result = new JsonResult();


            // var model = accomodationService.SearchAccomodationsAvailability(bookingViewModel.AccomodationPackageId);
            var model = accomodationService.CheckAccomodationsAvailability(bookingViewModel.AccomodationPackageId, bookingViewModel.CheckIn, bookingViewModel.CheckOut);
            //var bdate = model.Select(a => a.Bookings.Select(b => b.FromDate));

            //lock (Lock)
            //{
            bool state = true;

            if (model != null && model.Count() >= bookingViewModel.NoOfAccomodation)
            {
                //Session["Key"] = "Key";
                var roomNos = model.Select(a => a.Id).Take(bookingViewModel.NoOfAccomodation).ToList();
                var ids = string.Join(",", roomNos);

                //bookingService.UpdateState(roomNos , state);

                result.Data = new { success = true, message = "Rooms Availabale", data = ids };

            }
            else if (model.Count() < bookingViewModel.NoOfAccomodation)
            {

                result.Data = new
                {
                    success = false,
                    message = "You Asked for " + bookingViewModel.NoOfAccomodation +
                              " Accomodations but " + model.Count() + " Accomodation Availabale Only , plz Try Again"
                };

            }
            else
            {
                result.Data = new { success = false, message = "NO Room(s) Availabale at the moment try again later!" };

            }
            //}
            // ccheck here wheteher room available or not
            return result;
        }


        public ActionResult UpdateAccomodationStatus(string ids)
        {
            if (ids != null && ids.Count() > 0)
            {
                var accomodationids = ids.Split(',').Select(a => int.Parse(ids)).ToList();

                // free room marked as Inprocess=true here
                bookingService.UpdateState(accomodationids, false);
            }


            return RedirectToAction("Index", "Home");
        }









        // public ActionResult BookingAccomodation(BookingGetViewModel bookingViewModel)
        //{
        //    bookingViewModel.AccomodationPackage =
        //        accomodationPackagesService.GetAccomodationPackageById(bookingViewModel.AccomodationPackageId);
        //    var model = bookingViewModel;
        //    // ccheck here wheteher room available or not
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult BookingAccomodationCompleted(BookingPostViewModel bookingViewModel)
        //{
        //    bookingViewModel.AccomodationPackage =
        //        accomodationPackagesService.GetAccomodationPackageById(bookingViewModel.AccomodationPackageId);



        //    List<BookingDetail> bookingDetails= new List<BookingDetail>();

        //    foreach (var id in bookingViewModel.AccomodationId)
        //    {
        //    BookingDetail detailObj = new BookingDetail()
        //    {
        //        AccomodationId = id,
        //    };

        //        bookingDetails.Add(detailObj);
        //    }

        //    Booking obj = new Booking()
        //    {
        //        GuestName = bookingViewModel.GuestName,
        //        FromDate = bookingViewModel.CheckIn,
        //        ToDate = bookingViewModel.CheckOut,
        //        Duration = bookingViewModel.StayDuration,
        //        BreakFast = bookingViewModel.BreakFast,
        //        Adult = bookingViewModel.Adults,
        //        Children = bookingViewModel.Children,
        //        NoOfAccomodation = bookingViewModel.NoOfAccomodation,
        //        Email = bookingViewModel.Email,
        //        Phone = bookingViewModel.Phone,
        //        Address = bookingViewModel.Address,
        //        SpecialNote = bookingViewModel.SpecialNote,
        //        BookingDetails = bookingDetails


        //    };


        //    var bookingNo = bookingService.CreateBooking(obj , bookingViewModel.AccomodationPackageId);
        //    // ccheck here wheteher room available or not
        //    return View(bookingNo);
        //}

        //[HttpPost]
        //public JsonResult CheckAccomodationPackage(BookingGetViewModel bookingViewModel)
        //{
        //    JsonResult result = new JsonResult();

        //   // var model = accomodationService.SearchAccomodationsAvailability(bookingViewModel.AccomodationPackageId);
        //    var model = accomodationService.CheckAccomodationsAvailability(bookingViewModel.AccomodationPackageId, bookingViewModel.CheckIn, bookingViewModel.CheckOut);
        //    //var bdate = model.Select(a => a.Bookings.Select(b => b.FromDate));

        //    if (model != null && model.Count() >= bookingViewModel.NoOfAccomodation)
        //    {
        //        var roomNos = model.Select(a=>a.Id).Take(bookingViewModel.NoOfAccomodation);
        //        result.Data = new { success = true, message = "Rooms Availabale" , data= roomNos};

        //    }
        //    else if (model != null && model.Count() < bookingViewModel.NoOfAccomodation)
        //    {
        //        result.Data = new { success = false, message = "You Asked for "+ bookingViewModel.NoOfAccomodation +
        //                                                       " Accomodations but "+ model.Count() +" Accomodation Availabale Only" };

        //    }
        //    else
        //    {
        //        result.Data = new { success = false, message = "Rooms not Availabale" };

        //    }
        //    // ccheck here wheteher room available or not
        //    return result;
        //}
    }
}