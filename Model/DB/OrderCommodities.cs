using System;
using System.Collections.Generic;
using System.Text;

namespace WebCustomerApp.Models
{
    public class OrderCommodities
    {
        public int CommodityId { get; set; }
        public Commodity Commodity { get; set;}
        public  int OrderId { get; set; }
        public OrderUser OrderUser { get; set; }
    }
}
