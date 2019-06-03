using Model.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.BasketViewModels
{
    public class BasketCommoditiesUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public IEnumerable<CommodityBasketViewModel> CommodityUser { get; set; }
    }
}
