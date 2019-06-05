using System;
using System.Collections.Generic;
using System.Text;

namespace WebCustomerApp.Models
{
    public class ReceiptCommodities
    {
        public int CommodityId { get; set; }
        public Commodity Commodity { get; set; }
        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }
        public int Amount { get; set; }
    }
}
