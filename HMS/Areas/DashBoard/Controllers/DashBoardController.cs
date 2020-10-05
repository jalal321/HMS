using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HMS.Areas.DashBoard.ViewModels;
using HMS.Entities;
using HMS.Services;


namespace HMS.Areas.DashBoard.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard/DashBoard
        List<int> iDs = new List<int>();
        DashBoardService dashBoardService = new DashBoardService();
        public ActionResult Index()
        {
            DashBoardViewModel model = new DashBoardViewModel();

            model.TotalAccomodationPackages = dashBoardService.AccomodationPackagesCount(); 
            model.TotalAccomodations = dashBoardService.AccomodationsCount(); 
            model.TotalCheckedIn = dashBoardService.TotalAccomodationsCheckedIn(); 
            model.TotalReserved = dashBoardService.TotalAccomodationsReserved(); 
            model.ArrivalExpetcedToday = dashBoardService.ArrivalsExpetced(); 
            model.DepartureExpectedToday = dashBoardService.DeparturesExpected(); 
            model.CompleteBookingDetail = dashBoardService.ComepleteBookingDetail(); 
            

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult GetMessages()
        {
            var model = dashBoardService.GetMessages();
            return View(model);
        }
        
        [ChildActionOnly]
        public ActionResult GetBookingsReserved()
        {
            var model = dashBoardService.GetBookingsReserved();
            return View(model);
        }


        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();

            List<Picture> pics = new List<Picture>();

            DashBoardService dashBoardService = new DashBoardService();

            if (Request.Files != null)
            {
                var files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    var picture = files[i];

                    var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);

                    var loadFileName = Server.MapPath("~/images/site/") + fileName;

                    picture.SaveAs(loadFileName);

                    var picObject = new Picture() {URL = fileName};

                    if (dashBoardService.SavePicture(picObject))
                    {
                        pics.Add(picObject);
                        iDs.Add(picObject.Id);
                    }
                }

                result.Data = pics;
            }
            else
            {
                result.Data = "File cannot be null!";
            }

           
            return result;
        }
    }
}