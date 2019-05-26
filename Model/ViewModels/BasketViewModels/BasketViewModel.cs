using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCustomerApp.Models;

namespace Model.ViewModels.BasketViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName{ get; set; }
     
    }
}
