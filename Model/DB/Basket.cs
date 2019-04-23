using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DB
{
    public class Basket
    {
        public int Id { get; set; }
        public ICollection<BasketCommodity> BasketCommodities { get; set; }

        public string Description { get; set; }

    }
}
