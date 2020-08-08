using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HMS.Entities;
using HMS.Services;
using HMS.ViewModels;

namespace HMS.Controllers
{
    public class AccomodationController : Controller
    {
        // GET: Accomodation
        public ActionResult Index(string searchTerm , int? typeid , int? packageid)
        {
            AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
            AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
            AccomodationService accomodationService = new AccomodationService();
            AccomodationViewModel model = new AccomodationViewModel();


            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackages(searchTerm , typeid);
            model.searchTerm = searchTerm;
            model.CurrentAccomodationType = typeid;
            model.CurrentAccomodationPackage = packageid;
           

          
                model.Accomodations =
                   accomodationService.SearchAccomodations(searchTerm, typeid , packageid); 
            

            //if (typeid > 0)
            //{
            //    model.Accomodations =
            //        accomodationService.SearchAccomodations(searchTerm, typeid);
            //    model.AccomodationPackages = model.AccomodationPackages.Where(a => a.AccomodationTypeId == typeid).ToList();


               
            //}
            //else
            //{
            //    model.Accomodations =
            //       accomodationService.SearchAccomodations(searchTerm, typeid);
            //    model.AccomodationPackages = model.AccomodationPackages.ToList();

            //}
            //if (packageid > 0)
            //{
            //    model.Accomodations =
            //        accomodationService.SearchAccomodations(searchTerm, typeid, packageid);
            //    model.AccomodationPackages = model.AccomodationPackages.ToList();


            //}

            //else
            //{
            //    model.Accomodations =
            //       accomodationService.SearchAccomodations(searchTerm, typeid, packageid);
            //    model.AccomodationPackages = model.AccomodationPackages.ToList();


            //}
            

            
           
            return View(model);
        }

        //public List<AccomodationPackage> GetAccomodationPackages(int? typeid)
        //{
        //    AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();

        //    var packages = accomodationPackagesService.SearchAccomodationPackages(typeid);

        //    return packages.ToList();
        //}
    }
}