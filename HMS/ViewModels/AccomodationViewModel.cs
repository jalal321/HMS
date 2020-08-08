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
}