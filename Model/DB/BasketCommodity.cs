using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DB
{
   public class BasketCommodity
    {
        public  int? BasketId { get; set; }
        public Basket Basket { get; set; }

        public int? CommodityId { get; set; }
        public  Commodity Commodity { get; set; }
    }
}
