using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.OrderCommodityViewModels;
using Model.ViewModels.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class OrderCommoditiesManager: BaseManager,IOrderCommoditiesManager
    {
        public OrderCommoditiesManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public void Delete(int id)
        {
            OrderCommodities orderCom = unitOfWork.OrderCommoditieses.GetById(id);
            unitOfWork.OrderCommoditieses.Delete(orderCom);
            unitOfWork.Save();
        }

        public void Insert(OrderCommodityViewModel item)
        {
            OrderCommodities orderCommodities = mapper.Map<OrderCommodityViewModel, OrderCommodities>(item);
            unitOfWork.OrderCommoditieses.Insert(orderCommodities);
            unitOfWork.Save();
        }

        public void Update(OrderCommodityViewModel item)
        {
            OrderCommodities orderCommodity = mapper.Map<OrderCommodityViewModel, OrderCommodities>(item);
            unitOfWork.OrderCommoditieses.Update(orderCommodity);
            unitOfWork.Save();

            var orderCommodities = unitOfWork.OrderCommoditieses.Get(oc => oc.OrderId == item.OrderId);

            foreach (var it in orderCommodities)
            {
                if(it.IsConfirmeds ==false)
                {
                    return;
                }
            }
           OrderUser order = unitOfWork.OrderUsers.GetById(item.OrderId);
            order.IsConfirmed = true;
            unitOfWork.Save();
        }

        public IEnumerable<OrderUserViewModel> ShowOrderUsers(string userId)
        {
            var order = unitOfWork.OrderUsers.Get(b => b.UserId == userId);
            return mapper.Map<IEnumerable<OrderUser>, IEnumerable<OrderUserViewModel>>(order);

        }

        public IEnumerable<OrderCommodityViewModel> ShowOrderForModerUnAccepted(int moderId)
        {
            var comms = unitOfWork.OrderCommoditieses.Get(b => b.Commodity.ModeratorId == moderId).Where(oc=>oc.IsConfirmeds==false);

            return mapper.Map<IEnumerable<OrderCommodities>, IEnumerable<OrderCommodityViewModel>>(comms);

            
        }

        public IEnumerable<OrderCommodityViewModel> ShowAllOrderForModer(int moderId)
        {


            //unitOfWork.OrderUsers.Insert(new OrderUser()
            //{
            //    DataOrder = DateTime.Now,
            //    IsConfirmed = false,
            //    UserId = "5a8af8de-db7e-48ac-9617-6a94fa446d1d"
            //});
            //unitOfWork.Save();
            //var newOrder = unitOfWork.OrderUsers.Get(b => b.UserId == "5a8af8de-db7e-48ac-9617-6a94fa446d1d").First();
            //OrderCommodities orderCommodities = new OrderCommodities()
            //{
            //    CommodityId = 1,
            //    OrderId = newOrder.Id,
            //    IsConfirmeds = false
            //};
            //unitOfWork.OrderCommoditieses.Insert(orderCommodities);
            //unitOfWork.Save();

            var comms = unitOfWork.OrderCommoditieses.Get(b => b.Commodity.ModeratorId == moderId);
            var commV = mapper.Map<IEnumerable<OrderCommodities>, IEnumerable<OrderCommodityViewModel>>(comms);
            foreach (var  item in commV)
            {
                var commodity = unitOfWork.Commodities.GetById(item.CommodityId);
                item.CommodityName = commodity.Name;
            }
            return commV;
        }
    }
}
