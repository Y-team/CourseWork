using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace WebCustomerApp.Models
{
   public class Moderator
    {
       public int Id { get; set; }
       public string UserId { get; set; }
       public ApplicationUser ApplicationUser { get; set; }
       public string NameCompany { get; set; }
    }
}
