using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Areas.ViewModels;
using HMS.Services;
using HMS.Data;
using HMS.Entities;

namespace HMS.Areas.DashBoard.Controllers
{
    

    public class AccomodationTypesController : Controller
    {

        private AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        
        // GET: DashBoard/AccomodationType
        public ActionResult Index(string searchTerm)
        {
            AccomodationTypesListingViewModel model = new AccomodationTypesListingViewModel();

            //model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            model.AccomodationTypes = accomodationTypesService.SearchAccomodationTypes(searchTerm);
            model.SearchTerm = searchTerm;

            return View(model);
        }
        
        public ActionResult Listing()
        {
            AccomodationTypesListingViewModel model = new AccomodationTypesListingViewModel();
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            return PartialView("_Listing", model);
        }

        [HttpGet]
        public ActionResult Action(int? ID , bool isDelete = false)
        {
            AccomodationTypesActionViewModel model = new AccomodationTypesActionViewModel();
            ViewBag.isDelete = isDelete;

            if (ID.HasValue && isDelete)
            {
                //delete here
                AccomodationType accomodationType= accomodationTypesService.GetAccomodationTypeById(ID);
                model.Id = accomodationType.Id;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;
            }

            else if (ID.HasValue && isDelete == false)
            {
                //edit here
                AccomodationType accomodationType = accomodationTypesService.GetAccomodationTypeById(ID);
                model.Id = accomodationType.Id;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;
            }
            else
            {
                //new entry
            }
            return PartialView("_Action" , model);
        }

        [HttpPost]

        public JsonResult Action(AccomodationType model , bool isDeleted = false)
        {

             JsonResult json = new JsonResult();
            var result = false;

            if (model.Id > 0 && isDeleted == false)
            {
                //edit here
              result = accomodationTypesService.UpdateAccomodationTypes(model);
            }
            else if (model.Id > 0 && isDeleted == true)
            {
                //delete here
                result = accomodationTypesService.DeleteAccomodationTypes(model.Id);  
            }
             
            else
            {
                //first create object then add
            AccomodationType accomodationType = new AccomodationType();
            accomodationType.Name = model.Name;
            accomodationType.Description = model.Description;

            result = accomodationTypesService.SaveAccomodationTypes(accomodationType);
            }
            
            if (result)
            {
                json.Data = new { success = true };
            }
            else
            {
                json.Data = new { success = false, Messag = "Unable to Perform Operation in Accomodation Type." };
            }

            return json; 

           
        }
        
    }
}
