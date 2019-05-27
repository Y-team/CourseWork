using System;
using System.Collections.Generic;
using System.Text;

namespace WebCustomerApp.Models
{
   public class Commodity
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public bool IsConfirmed { get; set; }

        public decimal Price { get; set; }

       // public ICollection <Photo> Photo { get; set; }
        public ICollection<OrderCommodities> OrderCommoditieses { get; set; }
        public ICollection<BasketCommodities> BasketCommoditieses { get; set; }
       
        public virtual LongDescription LongDescription { get; set; }
        public  int ModeratorId { get; set; }
        public  Moderator Moderator { get; set; }
   }
}
