using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.Ajax.Utilities;

namespace HMS.Controllers
{
    public class AccomodationController : Controller
    {

        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        AccomodationService accomodationService = new AccomodationService();

        // GET: Accomodation
        public ActionResult Index(string searchTerm, int? typeid, int? packageid)
        {
           
            AccomodationViewModel model = new AccomodationViewModel();


            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackages(searchTerm, typeid);
            model.searchTerm = searchTerm;
            model.CurrentAccomodationType = typeid;
            model.CurrentAccomodationPackage = packageid;



            model.Accomodations =
               accomodationService.SearchAccomodations(searchTerm, typeid, packageid);

            return View(model);
        }

        public ActionResult AccomodationPackageDetail(int id)
        {
            AccomodationPackageDetailViewModel model = new AccomodationPackageDetailViewModel();
            model.AccomodationPackage = accomodationPackagesService.GetAccomodationPackageById(id);

            return View(model);
        }

        public ActionResult AccomodationPackages(string searchTerm, int? typeid)
        {
         
            //AccomodationService accomodationService = new AccomodationService();
            AccomodationPackagesViewModel model = new AccomodationPackagesViewModel();


            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackages(searchTerm, typeid);
            model.searchTerm = searchTerm;
            model.CurrentAccomodationType = typeid;
            //model.CurrentAccomodationPackage = packageid;



            //model.Accomodations =accomodationService.SearchAccomodations(searchTerm, typeid, packageid);

            return View(model);
        }


        [HttpPost]
        public ActionResult SearchAccomodation(SearchAccomodationViewModel model , bool tableview = false)
        {
            ViewBag.view = tableview;
            //HomeViewModel model = new HomeViewModel();
                List<AccomodationPackageCountViewModel> accomodationPackageCountViewModels = new List<AccomodationPackageCountViewModel>();


            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes().ToList();
            var accomodationPacakges = accomodationService.CheckAccomodationsAvailability(null, model.CheckIn , model.CheckOut);
            foreach (var item in accomodationPacakges.DistinctBy(a=>a.AccomodationPackage.Id))
            {

                //var count = accomodationService.CountAccomodationByPackage(item.AccomodationPackage.Id);
                var count = accomodationPacakges.Count(b => b.AccomodationPackage.Id == item.AccomodationPackage.Id);
                if (count > 0)
                {
                    AccomodationPackageCountViewModel obj = new AccomodationPackageCountViewModel();
                    obj.AccomodationPackages = item.AccomodationPackage;
                    obj.RoomCount = count;
                    accomodationPackageCountViewModels.Add(obj);

                }
            }

            model.AccomodationPackageCountViewModels = accomodationPackageCountViewModels;
           
            return View(model);
        }

         [HttpPost]
        public ActionResult CheckAccomodationAvailability(CheckAccomodationAvailabilityViewModel model, bool tableview = false)
        {

            ViewBag.view = tableview;
            List<AccomodationPackageCountViewModel> accomodationPackageCountViewModels = new List<AccomodationPackageCountViewModel>();

            //var accomodations = accomodationService.SearchAccomodationsAvailability(model.AccomodationType);
            var accomodationPacakges = accomodationService.CheckAccomodationsAvailability(null, model.CheckIn, model.CheckOut , model.AccomodationType);
            foreach (var item in accomodationPacakges.DistinctBy(a => a.AccomodationPackage.Id))
            {

                //var count = accomodationService.CountAccomodationByPackage(item.AccomodationPackage.Id);
                var count = accomodationPacakges.Count(b => b.AccomodationPackage.Id == item.AccomodationPackage.Id);
                if (count > 0)
                {
                    AccomodationPackageCountViewModel obj = new AccomodationPackageCountViewModel();
                    obj.AccomodationPackages = item.AccomodationPackage;
                    obj.RoomCount = count;
                    accomodationPackageCountViewModels.Add(obj);

                }
            }


            return View("_CheckAccomodationAvailability", accomodationPackageCountViewModels);
        }
    }
}