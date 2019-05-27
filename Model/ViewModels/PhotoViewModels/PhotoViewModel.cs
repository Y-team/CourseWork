using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace Model.ViewModels.PhoneViewModels
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public int CommodityId { get; set; }
        public byte[] Paint { get; set; }
        public string Name { get; set; }
    }
}
