using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace WebCustomerApp.Models
{
   public class Photo
   {
       public int Id { get; set; }
       public int CommodityId { get; set; }
       public  Commodity Commodity { get; set; }
       public byte[] Paint { get; set; }
       public string Name { get; set; }
    }
}
