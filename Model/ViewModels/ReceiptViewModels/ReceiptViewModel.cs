using Model.ViewModels.CommodityViewModels;
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

        public int RequiredInformationId { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Display(Name = "AddressLine1")]
        public string AddressLine1 { get; set; }
        [Display(Name = "AddressLine2")]
        public string AddressLine2 { get; set; }
        [Display(Name = "PostalCode")]
        public string PostalCode { get; set; }
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Display(Name = "ShippingMethod")]
        public string ShippingMethod { get; set; }
        [Display(Name = "PaymentMethod")]
        public string PaymentMethod { get; set; }

        public List<CommodityBasketViewModel> CommodityUser { get; set; }
    }
}
