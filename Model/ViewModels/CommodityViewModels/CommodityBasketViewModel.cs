using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.CommodityViewModels
{
   public class CommodityBasketViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool IsConfirmed { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        public int ModeratorId { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        [Display(Name = "Description")]
        [StringLength(150)]
        public string Description { get; set; }

        [Display(Name = "Photo")]
        public string PhotoName { get; set; }

        public IFormFile PhotoFile { get; set; }

        [Required(ErrorMessage = "The Amount field is required.")]
        [Display(Name = "Amount")]
        [Range(1, 20, ErrorMessage = "Amount  range {0} ... {1}")]
        public int Amount { get; set; }
    }
}
