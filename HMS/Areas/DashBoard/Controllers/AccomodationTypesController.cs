using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Areas.ViewModels;
using HMS.Services;
using HMS.Data;

namespace HMS.Areas.DashBoard.Controllers
{
    

    public class AccomodationTypesController : Controller
    {

        private AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        HMSContext Context = new HMSContext();
        // GET: DashBoard/AccomodationType
        public ActionResult Index()
        {
            
            return View();
        }
        
        public ActionResult Listing()
        {
            AccomodationTypesListingViewModel model = new AccomodationTypesListingViewModel();
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            return PartialView("_Listing", model);
        }

        public ActionResult Action()
        {
            AccomodationTypesActionViewModel model = new AccomodationTypesActionViewModel();
            return PartialView("_Action" , model);
        }
        
    }
}