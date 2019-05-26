using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebCustomerApp.Models
{
   public class Basket
    {
        public  int Id { get; set; }
        public string Description { get; set; }
        public  string UserId { get; set; }
        public  ApplicationUser ApplicationUser { get; set; }
        public ICollection<BasketCommodities> BasketCommoditieses { get; set; }
    }
}
