using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HMS.Areas.DashBoard.ViewModels;
using HMS.Code;
using HMS.Data;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.Ajax.Utilities;

namespace HMS.Areas.DashBoard.Controllers
{
    public class BookingsController : Controller
    {
        BookingService bookingService = new BookingService();
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        // GET: DashBoard/Bookings
        public ActionResult Index(string searchTerm, int? accomodationPackageId, int? page , int status = 0)
        {
            var statusString = (Enums.BookingStatus) status;
            BookingListingViewModel bookingListingViewModel = new BookingListingViewModel();
            bookingListingViewModel.Bookings = bookingService.SearchBookings(searchTerm , accomodationPackageId , statusString.ToString());
            bookingListingViewModel.totalRecord = bookingListingViewModel.Bookings.Count();
            //pagination logic start from here
            var pager = new Pager(bookingListingViewModel.totalRecord, page);

            bookingListingViewModel.Bookings =
                bookingListingViewModel.Bookings.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            bookingListingViewModel.SearchTerm = searchTerm;
            bookingListingViewModel.Status = (Enums.BookingStatus) status;
            bookingListingViewModel.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();
            bookingListingViewModel.AccomodationPackageId = accomodationPackageId;
            bookingListingViewModel.pager = pager;

            return View(bookingListingViewModel);
        }
        [HttpGet]
        public ActionResult Action(int? ID, bool isDelete = false)
        {
            BookingActionViewModel model = new BookingActionViewModel();
            ViewBag.isDelete = isDelete;


            if (ID.HasValue && isDelete)
            {
                //delete here
                model.Booking = bookingService.GetBookingById(ID);
                model.Id = ID;
                model.AccomodationPackageId = model.Booking.BookingDetails.FirstOrDefault().Accomodation.AccomodationPackageId;
                model.AccomodationPackageName = accomodationPackagesService.GetAccomodationPackageById(model.AccomodationPackageId).Name;
                //model.AccomodationPackagePictures = accomodationPackage.AccomodationPackagePictures;
            }

            else if (ID.HasValue && isDelete == false)
            {
                //edit here
                model.Booking = bookingService.GetBookingById(ID);
                model.Id = ID;
                model.AccomodationPackageId = model.Booking.BookingDetails.FirstOrDefault().Accomodation.AccomodationPackageId;
                model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();
                //model.Id = accomodationPackage.Id;
                //model.AccomodationTypeId = accomodationPackage.AccomodationTypeId;
                //model.Name = accomodationPackage.Name;
                //model.NoOfRoom = accomodationPackage.NoOfRoom;
                //model.FeePerNight = accomodationPackage.FeePerNight;
                //model.AccomodationPackagePictures = accomodationPackage.AccomodationPackagePictures;
            }
            else
            {
                model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();

                //new entry

            }
            //model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            return PartialView("_Action", model);
        }

        [HttpPost]

        public JsonResult Action(BookingActionViewModel model, bool isDeleted = false)
        {


            JsonResult json = new JsonResult();
            var result = false;
            var msg = "";
            if (model.Id > 0 && isDeleted == false)
            {
                //edit here

                if (model.Booking != null)
                {
                    result = bookingService.UpdateBooking(model.Booking);
                    msg = "Booking Edited Successfully";
                }
            }
            else if (model.Id > 0 && isDeleted == true)
            {
                //delete here
                //result = accomodationPackageService.DeleteAccomodationPackages(model.Id);
            } 
            
            else
            {
                //first create object then add
                var fee = accomodationPackagesService.GetAccomodationPackageById(model.AccomodationPackageId).FeePerNight;

                var roomsTotal = (model.Booking.NoOfAccomodation * fee) * model.Booking.Duration;
                var vatTax = (5 * roomsTotal) / 100;
                var tourismTax = 5 * (model.Booking.Duration * model.Booking.NoOfAccomodation);
                var breakFastTotals = 0;

                if (model.Booking.BreakFast == true)
                {
                breakFastTotals = ((model.Booking.Adult * 7) + (model.Booking.Children * 3)) * model.Booking.Duration * model.Booking.NoOfAccomodation;
                }

                var grandTotal = roomsTotal + vatTax + tourismTax + breakFastTotals;


                model.Booking.TotalAmount = grandTotal;
                msg = bookingService.CreateBooking(model.Booking, model.AccomodationPackageId);
                result = true;
            }

            if (result)
            {
                json.Data = new { success = true, Messag = msg };
            }
            else
            {
                json.Data = new { success = false, Messag = "Unable to Perform Operation in Accomodation Type." };
            }

            return json;

        }

        [HttpPost]

        public JsonResult OtherChargesAction(int id, bool isAddOtherCharges = false, decimal amount = 0, string details = null)
        {
            JsonResult json = new JsonResult();
            var result = false;

            
            if (id > 0 && isAddOtherCharges == true )
            {
                  try
            {
                //add other charges here
                BookingAdditionalFee bookingAdditionalFee = new BookingAdditionalFee()
                {
                    BookingId = id,
                    Name = details,
                    Fee = amount,
                    Status = 1
                };

                result = bookingService.AddOtherCharges(bookingAdditionalFee);

            }
                  catch (Exception)
                  {

                      throw;
                  }
           
            }


            if (id > 0 && isAddOtherCharges == false)
            {
                try
                {
                    result = bookingService.UpdateAddionalChargesStatus(id, 0);
                  
                }

                catch (Exception)
                {

                    throw;
                }
            }

            if (result)
            {
                json.Data = new { success = true, msg = "success" };
            }
            else
            {
                json.Data = new { success = false, Messag = "Unable to Perform Operation in Booking." };
            }
            return json;
        }

        // GET: DashBoard/Bookings/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Booking booking = db.Bookings.Find(id);
        //    if (booking == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booking);
        //}

        //// GET: DashBoard/Bookings/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: DashBoard/Bookings/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,GuestName,FromDate,ToDate,Duration,BreakFast,Adult,Children,NoOfAccomodation,Email,Phone,Address,SpecialNote")] Booking booking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Bookings.Add(booking);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(booking);
        //}

        //// GET: DashBoard/Bookings/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Booking booking = db.Bookings.Find(id);
        //    if (booking == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booking);
        //}

        //// POST: DashBoard/Bookings/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,GuestName,FromDate,ToDate,Duration,BreakFast,Adult,Children,NoOfAccomodation,Email,Phone,Address,SpecialNote")] Booking booking)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(booking).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(booking);
        //}

        //// GET: DashBoard/Bookings/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Booking booking = db.Bookings.Find(id);
        //    if (booking == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booking);
        //}

        //// POST: DashBoard/Bookings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Booking booking = db.Bookings.Find(id);
        //    db.Bookings.Remove(booking);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
