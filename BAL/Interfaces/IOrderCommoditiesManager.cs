using Model.ViewModels.OrderCommodityViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface IOrderCommoditiesManager
    {
        void Insert(OrderCommodityViewModel item);
        void Update(OrderCommodityViewModel item);
        void Delete(int id);
        IEnumerable<OrderCommodityViewModel> ShowAllOrderForModer(int moderId);
        IEnumerable<OrderCommodityViewModel> ShowOrderForModerUnAccepted(int moderId);
    }
}
