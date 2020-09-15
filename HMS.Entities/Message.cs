using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Entities
{
   public class Message
    {
       public int Id { get; set; }

       [Required]
       public string FirstName { get; set; }
       [Required]

       public string LastName { get; set; }
       [Required]

       public string Email { get; set; }
       [Required]

       public string Phone { get; set; }
       [Required]
       public string Subject { get; set; }
       [Required]
       [Display(Name = "Message")]
       public string UserMessage { get; set; }


    }
}
