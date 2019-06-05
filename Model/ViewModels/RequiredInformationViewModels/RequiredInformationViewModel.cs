using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.RequiredInformationViewModel
{
   public class RequiredInformationViewModel
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public int OrderId { get; set; }
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
        public int BasketId { get; set; }
    }
}
