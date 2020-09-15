using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HMS.Data;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
             
            //HMSContext context = new HMSContext();
            //var a = context.AccomodationTypes.ToList();
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


        public JsonResult GetAccomodationTypes()
        {
            //Json result = new Json();
            var model = accomodationTypesService.GetAllAccomodationTypesList();
           
            //result.Data = new {model , JsonRequestBehavior.AllowGet};
            //result.Data = model;

            return Json(model ,JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendMessages(Message message)
        {
            ViewBag.Message = "Your contact page.";

            var result = accomodationTypesService.SendMessage(message);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return HttpNotFound();
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