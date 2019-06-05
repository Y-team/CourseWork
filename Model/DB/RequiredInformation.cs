using System;
using System.Collections.Generic;
using System.Text;

namespace WebCustomerApp.Models
{
    public class RequiredInformation
    {
        public  int Id { get; set; }
        public int BasketId { get; set; }
        public  Receipt Receipt { get; set; }
        public int OrderId { get; set; }
        public string City { get; set; }
        public string FullName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public string ShippingMethod { get; set; }

        public string PaymentMethod { get; set; }
    }
}
