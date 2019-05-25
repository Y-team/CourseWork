using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels.OrderViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime DataOrder { get; set; }
        public DateTime DataConfirmed { get; set; }
    }
}
