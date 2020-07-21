using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.Areas.ViewModels
{
    public class AccomodationTypesListingViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
    }
    
    public class AccomodationTypesActionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}