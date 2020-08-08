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
   public class AccomodationService
    {
        public IEnumerable<Accomodation> GetAllAccomodations()
        {
            var context = new HMSContext();
            return context.Accomodations.AsEnumerable();
        }
        public IEnumerable<Accomodation> SearchAccomodations(string searchTerm, int? typeid , int? packageid)
        {

            var context = new HMSContext();
            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (typeid.HasValue && typeid.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackage.AccomodationTypeId == typeid.Value);
            }
            
            if (packageid.HasValue && packageid.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackage.Id == packageid.Value);
            }

            return accomodations.ToList();
        }
        public IEnumerable<Accomodation> SearchAccomodations(string searchTerm, int? accomodationPackageID)
        {

            var context = new HMSContext();
            var accomodations = context.Accomodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accomodationPackageID.HasValue && accomodationPackageID.Value > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackage.Id == accomodationPackageID.Value);
            }

            
            return accomodations.ToList();
        }
        public Accomodation GetAccomodationById(int? id)
        {
            var context = new HMSContext();
            return context.Accomodations.Find(id);
        }

        public bool SaveAccomodations(Accomodation accomodation)
        {
            var context = new HMSContext();
            context.Accomodations.Add(accomodation);
            var result = context.SaveChanges() > 0;

            return result;
        }
        public bool UpdateAccomodations(Accomodation accomodation)
        {
            var context = new HMSContext();
            var obj = context.Accomodations.Find(accomodation.Id);

            obj.Name = accomodation.Name;
            obj.AccomodationPackageId = accomodation.AccomodationPackageId;

            obj.Description = accomodation.Description;


            context.Entry(obj).State = EntityState.Modified;

            var result = context.SaveChanges() > 0;

            return result;
        }

        public bool DeleteAccomodations(int id)
        {
            var context = new HMSContext();
            var obj = context.Accomodations.Find(id);

            context.Entry(obj).State = EntityState.Deleted;
            var result = context.SaveChanges() > 0;

            return result;
        }
    }

}
