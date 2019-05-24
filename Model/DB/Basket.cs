using System;
using System.Collections.Generic;
using System.Text;

namespace WebCustomerApp.Models
{
   public class Basket
   {
        public  int Id { get; set; }
        public string Description { get; set; }
        public ICollection<BasketCommodities> BasketCommoditieses { get; set; }
    }
}
