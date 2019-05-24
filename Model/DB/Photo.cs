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
      /* private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
           System.Drawing.Imaging.ImageFormat formatOfImage)
       {
           byte[] Ret;
           try
           {
               using (MemoryStream ms = new MemoryStream())
               {
                   imageToConvert.Save(ms, formatOfImage);
                   Ret = ms.ToArray();
               }
           }
           catch (Exception) { throw; }
           return Ret;
       }*/
    }
}
