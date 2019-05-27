using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels.PhotoViewModels
{
    public class ImageViewModel
    {
        public int CommodityId { get; set; }
        public IFormFile PhotoFile { get; set; }
    }
}
