using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;
using WebCustomerApp.Models;

namespace WebCustomerApp.Models
{
   public class OrderUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime DataOrder { get; set; }
        public DateTime DataConfirmed { get; set; }
        public int RequiredInformationId { get; set; }

        public ICollection<OrderCommodities> OrderCommoditieses { get; set; }

    }
}
