using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
   public class Subscribers
    {
       public int Id { get; set; }

       [Required]
       [EmailAddress]
       public string Email { get; set; }
    }
}
