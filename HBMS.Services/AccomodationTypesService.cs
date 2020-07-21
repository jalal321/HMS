using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Data;
using HMS.Entities;

namespace HMS.Services
{
   public class AccomodationTypesService
    {

       public IEnumerable<AccomodationType> GetAllAccomodationTypes()
       {
           var context = new HMSContext();
           return context.AccomodationTypes.ToList(); 
       }
    }
}
