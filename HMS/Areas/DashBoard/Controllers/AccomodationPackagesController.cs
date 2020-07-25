using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Areas.DashBoard.ViewModels;
using HMS.Areas.ViewModels;
using HMS.Entities;
using HMS.Models;
using HMS.Services;
using HMS.ViewModels;

namespace HMS.Areas.DashBoard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        private AccomodationPackagesService accomodationPackageService = new AccomodationPackagesService();
        private AccomodationTypesService accomodationTypesService = new AccomodationTypesService();

        // GET: DashBoard/AccomodationType
        public ActionResult Index(string searchTerm, int? accomodationTypeID , int? page)
         {
            AccomodationPackagesListingViewModel model = new AccomodationPackagesListingViewModel();

            //var dummyItems = Enumerable.Range(1, 150).Select(x => "Item " + x);
            
			 

            //model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackages();
            model.AccomodationPackages = accomodationPackageService.SearchAccomodationPackages(searchTerm , accomodationTypeID);
            model.totalRecord = model.AccomodationPackages.Count();

            //pagination logic start from here
            var pager = new Pager(model.totalRecord, page);

            model.AccomodationPackages =
                model.AccomodationPackages.Skip((pager.CurrentPage - 1)*pager.PageSize).Take(pager.PageSize).ToList();

            model.SearchTerm = searchTerm;
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            model.AccomodationTypeID = accomodationTypeID;
            model.pager = pager;
           
            

            return View(model);
        }

        public ActionResult Listing()
        {
            AccomodationPackagesListingViewModel model = new AccomodationPackagesListingViewModel();
            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackages();
            return PartialView("_Listing", model);
        }

        [HttpGet]
        public ActionResult Action(int? ID, bool isDelete = false)
        {
            AccomodationPackagesActionViewModel model = new AccomodationPackagesActionViewModel();
            ViewBag.isDelete = isDelete;


            if (ID.HasValue && isDelete)
            {
                //delete here
                AccomodationPackage accomodationPackage = accomodationPackageService.GetAccomodationPackageById(ID);
                model.Id = accomodationPackage.Id;
                model.Name = accomodationPackage.Name;
                model.NoOfRoom = accomodationPackage.NoOfRoom;
                model.FeePerNight = accomodationPackage.FeePerNight;
            }

            else if (ID.HasValue && isDelete == false)
            {
                //edit here
               AccomodationPackage accomodationPackage = accomodationPackageService.GetAccomodationPackageById(ID);
                model.Id = accomodationPackage.Id;
                model.AccomodationTypeId = accomodationPackage.AccomodationTypeId;
                model.Name = accomodationPackage.Name;
                model.NoOfRoom = accomodationPackage.NoOfRoom;
                model.FeePerNight = accomodationPackage.FeePerNight;
            }
            else
            {
                //new entry
                
            }
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            return PartialView("_Action", model);
        }

        [HttpPost]

        public JsonResult Action(AccomodationPackage model, bool isDeleted = false)
        {

            JsonResult json = new JsonResult();
            var result = false;

            if (model.Id > 0 && isDeleted == false)
            {
                //edit here
                result = accomodationPackageService.UpdateAccomodationPackages(model);
            }
            else if (model.Id > 0 && isDeleted == true)
            {
                //delete here
                result = accomodationPackageService.DeleteAccomodationPackages(model.Id);
            }

            else
            {
                //first create object then add
                AccomodationPackage accomodationPackage = new AccomodationPackage();

                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;

                result = accomodationPackageService.SaveAccomodationPackages(accomodationPackage);
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