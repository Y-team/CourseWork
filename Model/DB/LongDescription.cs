using System;
using System.Collections.Generic;
using System.Text;

namespace WebCustomerApp.Models
{
   public class LongDescription
    {
        public  int Id { get; set; }
        public  string Description { get; set; }
        public  int CommodityId { get; set; }
        public Commodity Commodity { get; set; }
    }
}
