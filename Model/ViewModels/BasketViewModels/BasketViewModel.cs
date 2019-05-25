using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace Model.ViewModels.BasketViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserName{ get; set; }
     
    }
}
