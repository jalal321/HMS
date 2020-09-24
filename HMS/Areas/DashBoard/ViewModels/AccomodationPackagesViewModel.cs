using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;
using HMS.ViewModels;

namespace HMS.Areas.DashBoard.ViewModels
{
    public class AccomodationPackagesListingViewModel
    {
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

        public string SearchTerm { get; set; }

        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

        public int? AccomodationTypeID { get; set; }

        public Pager pager { get; set; }

        public int totalRecord { get; set; }
    }

    public class AccomodationPackagesActionViewModel
    {
        public List<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }

        public int Id { get; set; }

        public AccomodationPackage AccomodationPackage { get; set; }

        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

        public int AccomodationTypeId { get; set; }

        public AccomodationType AccomodationType { get; set; }

        public string Name { get; set; }

        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }

        public string PicIDs { get; set; }


    }


}