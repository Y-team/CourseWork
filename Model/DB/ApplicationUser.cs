using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebCustomerApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        
        public Basket Basket { get; set; }

        public  Moderator Moderator { get; set; }

        public ICollection<OrderUser> Orders { get; set; }
        public int InviteId { get; set; } = 0; // this field filled up when user has inviting to group//Delete
    }
}
