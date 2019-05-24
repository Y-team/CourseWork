using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebCustomerApp.Models;

namespace Model.DB
{
   public class PhoneRec
    {
        [Key]
        public int PhoneId { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
       
      
    }
}
