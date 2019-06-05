using Model.ViewModels.OrderCommodityViewModels;
using Model.ViewModels.RequiredInformationViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IOrderCommoditiesManager
    {
        void Insert(OrderCommodityViewModel item);
        void Update(OrderCommodityViewModel item);
        void AddNewOrder(RequiredInformationViewModel recInfo);
        void Delete(int commodityId, int orderId);
        IEnumerable<OrderCommodityViewModel> ShowAllOrderForModer(int moderId);
        IEnumerable<OrderCommodityViewModel> ShowOrderForModerUnAccepted(int moderId);
        OrderCommodityViewModel Confirmed(int CommodityId,int OrderId);
    }
}
