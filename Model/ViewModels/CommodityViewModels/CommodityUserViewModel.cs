﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.CommodityViewModels
{
  public  class CommodityUserViewModel
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

        [Display(Name = "Photo")]
        public string PhotoName { get; set; }
        
        public IFormFile PhotoFile { get; set; }
       
    }
}