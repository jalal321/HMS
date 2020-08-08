using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Data;
using HMS.Entities;

namespace HMS.Services
{
   public class DashBoardService
    {
       public bool SavePictureList(List<Picture> pictures)
       {
           HMSContext db = new HMSContext();
           foreach (var pic in pictures)
           {
               db.Pictures.Add(pic);
           }


           return db.SaveChanges() > 0; 
       }
       
       public bool SavePicture(Picture pictures)
       {
           HMSContext db = new HMSContext();
         
           db.Pictures.Add(pictures);
         
           return db.SaveChanges() > 0; 
       }
    }
}
