using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.CommodityViewModels
{
    class CommodityMainViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool IsConfirmed { get; set; }
        [Required(ErrorMessage = "The Price field is required.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "The Quantity field is required.")]
        [Display(Name = "Quantity")]
        [Range(20, 999, ErrorMessage = "Amount  range {0} ... {1}")]
        public int QuantityInStorage { get; set; }
        public int ModeratorId { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        [Display(Name = "LongDescription")]
        [StringLength(1500)]
        public string LongDescription { get; set; }

        public string PhotoName { get; set; }
    }
}
