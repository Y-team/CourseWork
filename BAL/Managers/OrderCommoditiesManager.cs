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
            if (orderCommodities.Amount <= 0)
            {
                orderCommodities.Amount = 1;
            }
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
            order.DataConfirmed = DateTime.Now;
            unitOfWork.OrderUsers.Update(order);
            unitOfWork.Save();


            #region Creating Receipt

            var orderId = order.Id;
            var orderComs = unitOfWork.OrderCommoditieses.Get().Where(b => b.OrderId == orderId);
            var orderUser = unitOfWork.OrderUsers.GetById(orderId);

            List<Commodity> commodities = new List<Commodity>();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var it in orderComs)
            {
                commodities.Add(unitOfWork.Commodities.GetById(it.CommodityId));
            }
            int Count = 5;
            var user = unitOfWork.Users.Get(u => u.Id == orderUser.UserId).FirstOrDefault();
            if (orderUser.IsConfirmed)
            {
                stringBuilder.AppendFormat("User: {0}", user.UserName).AppendLine();
                stringBuilder.AppendFormat("Name -- Count -- Price").AppendLine();
                foreach (var it in commodities)
                {
                    stringBuilder.AppendFormat("{0}--{1}--{2};", it.Name, Count, it.Price * Count).AppendLine();
                    //stringBuilder.AppendLine();    
                }
                stringBuilder.AppendFormat("Date: {0}", orderUser.DataConfirmed);
            }

            Receipt receipt = new Receipt()
            {
                DateCheck = orderUser.DataConfirmed,
                Description = stringBuilder.ToString(),
                UserId = orderUser.UserId
            };



            unitOfWork.Receipts.Insert(receipt);
            unitOfWork.OrderUsers.Delete(orderUser);
            unitOfWork.Save();

            #endregion
        }

        public IEnumerable<OrderUserViewModel> ShowOrderUsers(string userId)
        {
            var order = unitOfWork.OrderUsers.Get(b => b.UserId == userId);
            return mapper.Map<IEnumerable<OrderUser>, IEnumerable<OrderUserViewModel>>(order);

        }

        public IEnumerable<OrderCommodityViewModel> ShowOrderForModerUnAccepted(int moderId)


        {
            var comms = unitOfWork.OrderCommoditieses.Get(b => b.Commodity.ModeratorId == moderId).Where(oc=>oc.IsConfirmeds==false);

            var coms =mapper.Map<IEnumerable<OrderCommodities>, IEnumerable<OrderCommodityViewModel>>(comms);
            foreach (var item in coms)
            {
                item.CommodityName = unitOfWork.Commodities.GetById(item.CommodityId).Name;
            }
            return coms;
        }

        public IEnumerable<OrderCommodityViewModel> ShowAllOrderForModer(int moderId)
        {

            

            var comms = unitOfWork.OrderCommoditieses.Get(b => b.Commodity.ModeratorId == moderId);
            var commV = mapper.Map<IEnumerable<OrderCommodities>, IEnumerable<OrderCommodityViewModel>>(comms);
            foreach (var  item in commV)
            {
                var commodity = unitOfWork.Commodities.GetById(item.CommodityId);
                item.CommodityName = commodity.Name;
            }
            return commV;
        }

        public void AddNewOrder(int basketId)
        {
            var basket = unitOfWork.Baskets.GetById(basketId);
            if (basket == null) { return; }

            var commodities = unitOfWork.BasketCommoditieses.Get(b => b.BasketId == basketId);
            if (commodities == null) { return; }

            var order = new OrderUser()
            {
                UserId = basket.UserId,
                DataOrder = DateTime.Now,
                IsConfirmed = false
            };
            unitOfWork.OrderUsers.Insert(order);

            foreach (var item in commodities)
            {
                var newOrderCom = new OrderCommodities
                {
                    CommodityId = item.CommodityId,
                    OrderUser = order
                };

                unitOfWork.OrderCommoditieses.Insert(newOrderCom);
            }

            unitOfWork.Save();
           
        }

        public OrderCommodityViewModel Confirmed(int CommodityId, int OrderId)
        {
            var orderComm = unitOfWork.OrderCommoditieses.Get().Where(b => b.CommodityId == CommodityId && b.OrderId == OrderId).FirstOrDefault();

            orderComm.IsConfirmeds = true;

            return mapper.Map<OrderCommodities, OrderCommodityViewModel>(orderComm);
        }
    }
}
