using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Models.DB
{
   public class CommodityImage
    {
        public int Id { get; set; }
        public int? CommodityId { get; set; }
        public Commodity Commodity { get; set; }
        public string GoodImage { get; set; }
    }
}
