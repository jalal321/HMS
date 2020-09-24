using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.ViewModels
{
    public class AccomodationViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

        public IEnumerable<Accomodation> Accomodations { get; set; }

        public int? CurrentAccomodationType { get; set; }

        public int? CurrentAccomodationPackage { get; set; }

        public string searchTerm { get; set; }
    }  
    
    public class AccomodationPackageDetailViewModel
    {

        public AccomodationPackage AccomodationPackage { get; set; }

        
    } 
    
    public class AccomodationPackagesViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

        public int? CurrentAccomodationType { get; set; }

        public string searchTerm { get; set; }
    }

    public class CheckAccomodationAvailabilityViewModel
    {
        //public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

        public IEnumerable<Accomodation> Accomodations { get; set; }

        public int? AccomodationType { get; set; }

        //public int? CurrentAccomodationPackage { get; set; }
        
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }


    } 
        
        public class AccomodationPackageCountViewModel
    {

        public AccomodationPackage AccomodationPackages { get; set; }
        public int? RoomCount { get; set; }

    }

        public class SearchAccomodationViewModel
        {
            public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

            public List<AccomodationPackageCountViewModel> AccomodationPackageCountViewModels { get; set; }

            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public string Adult { get; set; }
            public string Children { get; set; }

        }

    }

