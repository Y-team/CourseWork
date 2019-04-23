using System;
using System.Collections.Generic;
using System.Text;
using Models.Enams;

namespace Models.DB
{
   public class Commodity
    {
       public int Id { get; set; }
       public ICollection<BasketCommodity> BasketCommodities { get; set; }

       public string Name { get; set; }
        public string Description { get; set; }
        public  TypeCommodity TypeCommodity { get; set; }
        public decimal Price { get; set; }

        public int Number { get; set; }
    }
}
