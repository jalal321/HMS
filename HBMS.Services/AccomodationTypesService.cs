using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using HMS.Data;
using HMS.Entities;

namespace HMS.Services
{
   public class AccomodationTypesService
    {

       public IEnumerable<AccomodationType> GetAllAccomodationTypes()
       {
           var context = new HMSContext();
           return context.AccomodationTypes.Include(a=>a.AccomodationPackages).AsEnumerable(); 
       } 
       
       public IEnumerable<AccomodationType> GetAllAccomodationTypesList()
       {
           var context = new HMSContext();
           return context.AccomodationTypes.ToList(); 
       }
       public IEnumerable<AccomodationType> SearchAccomodationTypes(string searchTerm)
       {
           var context = new HMSContext();
           var accomodationTypes = context.AccomodationTypes.AsQueryable();

           if (!string.IsNullOrEmpty(searchTerm))
           {
               accomodationTypes = accomodationTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
           }
           return accomodationTypes.AsEnumerable(); 
       }

       public AccomodationType GetAccomodationTypeById(int? id)
       {
           var context = new HMSContext();
           return context.AccomodationTypes.Find(id); 
       }
       
       public bool SaveAccomodationTypes(AccomodationType accomodationType)
       {
           var context = new HMSContext();
           context.AccomodationTypes.Add(accomodationType);
           var result = context.SaveChanges() > 0;

           return result; 
       }
       public bool UpdateAccomodationTypes(AccomodationType accomodationType)
       {
           var context = new HMSContext();
           var obj = context.AccomodationTypes.Find(accomodationType.Id);

           obj.Name = accomodationType.Name;
           obj.Description = accomodationType.Description;


           context.Entry(obj).State = EntityState.Modified;

           var result = context.SaveChanges() > 0;

           return result; 
       }
       
       public bool DeleteAccomodationTypes(int id)
       {
           var context = new HMSContext();
           var obj = context.AccomodationTypes.Find(id);
       
           context.Entry(obj).State = EntityState.Deleted;
           var result = context.SaveChanges() > 0;

           return result; 
       }
    }
}
