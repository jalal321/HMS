using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Areas.DashBoard.ViewModels;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;

namespace HMS.Areas.DashBoard.Controllers
{
    public class AccomodationController : Controller
    {
        private AccomodationService accomodationService = new AccomodationService();
        private AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        

        // GET: DashBoard/AccomodationType
        public ActionResult Index(string searchTerm, int? accomodationPackageID, int? page)
        {
            AccomodationListingViewModel model = new AccomodationListingViewModel();

            //var dummyItems = Enumerable.Range(1, 150).Select(x => "Item " + x);



            //model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackages();
            model.Accomodations = accomodationService.SearchAccomodations(searchTerm, accomodationPackageID);
            model.totalRecord = model.Accomodations.Count();

            //pagination logic start from here
            var pager = new Pager(model.totalRecord, page);

            model.Accomodations =
                model.Accomodations.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);

            model.SearchTerm = searchTerm;
            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();
            model.AccomodationPackageId = accomodationPackageID;
            model.pager = pager;



            return View(model);
        }

        public ActionResult Listing()
        {
            AccomodationListingViewModel model = new AccomodationListingViewModel();
            model.Accomodations = accomodationService.GetAllAccomodations();
            return PartialView("_Listing", model);
        }

        [HttpGet]
        public ActionResult Action(int? ID, bool isDelete = false)
        {
            AccomodationActionViewModel model = new AccomodationActionViewModel();
            ViewBag.isDelete = isDelete;


            if (ID.HasValue && isDelete)
            {
                //delete here
                Accomodation accomodation = accomodationService.GetAccomodationById(ID);
                model.Id = accomodation.Id;
                model.AccomodationPackageId = accomodation.AccomodationPackageId;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
                
            }

            else if (ID.HasValue && isDelete == false)
            {
                //edit here
                Accomodation accomodation = accomodationService.GetAccomodationById(ID);
                model.Id = accomodation.Id;
                model.AccomodationPackageId = accomodation.AccomodationPackageId;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
            }
            else
            {
                //new entry

            }
            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();
            return PartialView("_Action", model);
        }

        [HttpPost]

        public JsonResult Action(Accomodation model, bool isDeleted = false)
        {

            JsonResult json = new JsonResult();
            var result = false;

            if (model.Id > 0 && isDeleted == false)
            {
                //edit here
                result = accomodationService.UpdateAccomodations(model);
            }
            else if (model.Id > 0 && isDeleted == true)
            {
                //delete here
                result = accomodationService.DeleteAccomodations(model.Id);
            }

            else
            {
                //first create object then add
                Accomodation accomodation = new Accomodation();

                accomodation.AccomodationPackageId = model.AccomodationPackageId;
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;
                

                result = accomodationService.SaveAccomodations(accomodation);
            }

            if (result)
            {
                json.Data = new { success = true };
            }
            else
            {
                json.Data = new { success = false, Messag = "Unable to Perform Operation." };
            }

            return json;


        }
    }
}