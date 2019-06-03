using System;
using System.Collections.Generic;
using System.Text;

namespace WebCustomerApp.Models
{
    public class BasketCommodities
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        public int CommodityId { get; set; }
        public  Commodity Commodity { get; set; }

        public int Amount { get; set; }
    }
}
