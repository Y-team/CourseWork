using System;
using System.Collections.Generic;
using System.Text;

namespace WebCustomerApp.Models
{
    public class Receipt // our check for order
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime DateCheck { get; set; }
    }
}
