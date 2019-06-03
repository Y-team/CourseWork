using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.ReceiptViewModels
{
    public class ReceiptViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime DateCheck { get; set; }
    }
}
