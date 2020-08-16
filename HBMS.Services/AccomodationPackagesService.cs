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
           return context.AccomodationPackages.ToList(); 
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
           

           context.AccomodationPackagePictures.RemoveRange(obj.AccomodationPackagePictures);
           context.AccomodationPackagePictures.AddRange(accomodationPackage.AccomodationPackagePictures);

         

           obj.Name = accomodationPackage.Name;
           obj.NoOfRoom = accomodationPackage.NoOfRoom;
           obj.FeePerNight = accomodationPackage.FeePerNight;
           obj.AccomodationTypeId = accomodationPackage.AccomodationTypeId;
           
           //obj.AccomodationPackagePictures = accomodationPackage.AccomodationPackagePictures;
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


//obj.AccomodationPackagePictures.Clear();
//context.SaveChanges();
//obj.AccomodationPackagePictures.Clear();
//foreach (var p in obj.AccomodationPackagePictures.ToList())
//{
//    obj.AccomodationPackagePictures.Remove(p);
//    //obj.AccomodationPackagePictures.RemoveRange(obj.AccomodationPackagePictures.Find(aId) , obj.AccomodationPackagePictures.Count);

//}

//foreach (var p in accomodationPackage.AccomodationPackagePictures.ToList())
//{
//    obj.AccomodationPackagePictures.Add(p);
//    //obj.AccomodationPackagePictures.RemoveRange(obj.AccomodationPackagePictures.Find(aId) , obj.AccomodationPackagePictures.Count);

//}