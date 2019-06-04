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
        void AddNewOrder(int basketId);
        void Delete(int commodityId, int orderId);
        IEnumerable<OrderCommodityViewModel> ShowAllOrderForModer(int moderId);
        IEnumerable<OrderCommodityViewModel> ShowOrderForModerUnAccepted(int moderId);
        OrderCommodityViewModel Confirmed(int CommodityId,int OrderId);
    }
}
