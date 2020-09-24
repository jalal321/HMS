using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HMS.Entities;

namespace HMS.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
    } 
    
    public class HomeSearchAccomodationViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string Adult { get; set; }
        public string Children { get; set; }
    }
}