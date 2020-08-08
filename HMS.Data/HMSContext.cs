using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace HMS.Data
{
    public class HMSContext : DbContext
    {

        public HMSContext()
            : base("HMSConnectionString")
        {
                
        }
         
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<AccomodationPicture> AccomodationPictures { get; set; }
        public DbSet<AccomodationPackage> AccomodationPackages { get; set; }
        public DbSet<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }
        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }


}
