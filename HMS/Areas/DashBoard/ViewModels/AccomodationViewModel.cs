using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;
using HMS.ViewModels;

namespace HMS.Areas.DashBoard.ViewModels
{
    public class AccomodationListingViewModel
    {
        public IEnumerable<Accomodation> Accomodations { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

        public int? AccomodationPackageId { get; set; }

        public Pager pager { get; set; }

        public int totalRecord { get; set; }
    }

    public class AccomodationActionViewModel
    {
        public int Id { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public int AccomodationPackageId { get; set; }
        public AccomodationPackage AccomodationPackage { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }


}