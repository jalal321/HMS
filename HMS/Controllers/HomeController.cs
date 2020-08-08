using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Data;
using HMS.Services;
using HMS.ViewModels;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
            HomeViewModel model = new HomeViewModel();
             
            //HMSContext context = new HMSContext();
            //var a = context.AccomodationTypes.ToList();
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes().Take(2).ToList();
            
            return View(model);
        } 
        
        [HttpPost]
        public ActionResult SearchAccomodation(HomeSearchAccomodationViewModel model)
        {
            AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
            //HomeViewModel model = new HomeViewModel();
             
           
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes().ToList();
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult DiningAndBar()
        {
            ViewBag.Message = "";

            return View();
        }


        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    context.Dispose();
            //}
            base.Dispose(disposing);
        }
    }

}