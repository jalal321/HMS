using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Data;
using HMS.Entities;

namespace HMS.Services
{
    public class AccomodationPackagesService
    {
          public IEnumerable<AccomodationPackage> GetAllAccomodationPackages()
       {
           var context = new HMSContext();
           return context.AccomodationPackages.AsEnumerable(); 
       }
          public IEnumerable<AccomodationPackage> SearchAccomodationPackages(string searchTerm , int? accomodationTypeid)
       {
              
           var context = new HMSContext();
           var accomodationPackages = context.AccomodationPackages.AsQueryable();

           if (!string.IsNullOrEmpty(searchTerm))
           {
               accomodationPackages = accomodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
           }

           if ( accomodationTypeid.HasValue && accomodationTypeid.Value > 0)
           {
               accomodationPackages = accomodationPackages.Where(a => a.AccomodationTypeId == accomodationTypeid.Value);
           }
           return accomodationPackages.ToList(); 
       }

       public AccomodationPackage GetAccomodationPackageById(int? id)
       {
           var context = new HMSContext();
           return context.AccomodationPackages.Find(id); 
       }

       public bool SaveAccomodationPackages(AccomodationPackage accomodationPackage)
       {
           var context = new HMSContext();
           context.AccomodationPackages.Add(accomodationPackage);
           var result = context.SaveChanges() > 0;

           return result; 
       }
       public bool UpdateAccomodationPackages(AccomodationPackage accomodationPackage)
       {
           var context = new HMSContext();
           var obj = context.AccomodationPackages.Find(accomodationPackage.Id);

           obj.Name = accomodationPackage.Name;
           obj.NoOfRoom = accomodationPackage.NoOfRoom;
          
           obj.FeePerNight = accomodationPackage.FeePerNight;


           context.Entry(obj).State = EntityState.Modified;

           var result = context.SaveChanges() > 0;

           return result; 
       }

       public bool DeleteAccomodationPackages(int id)
       {
           var context = new HMSContext();
           var obj = context.AccomodationPackages.Find(id);

           context.Entry(obj).State = EntityState.Deleted;
           var result = context.SaveChanges() > 0;

           return result;
       }
    }
}
