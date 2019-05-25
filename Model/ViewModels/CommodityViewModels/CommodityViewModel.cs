using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels.CommodityViewModels
{
    public class CommodityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsConfirmed { get; set; }

        public decimal Price { get; set; }

        public int ModeratorId { get; set; }
    }
}
