using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HMS.Data
{
    public class HMSContextClass : DbContext
    {

        public HMSContextClass(): base("HMSConnectionString")
        {
                
        }

        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
    }


}
