using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.OrderCommodityViewModels;
using Model.ViewModels.OrderViewModels;
using Model.ViewModels.RequiredInformationViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using WebCustomerApp.Models;
using WebCustomerApp.Models.AccountViewModels;
using WebCustomerApp.Services;

namespace BAL.Managers
{
    public class OrderCommoditiesManager: BaseManager,IOrderCommoditiesManager
    {
       
        public OrderCommoditiesManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
       
        public void Delete(int commodityId, int orderId)
        {
            OrderCommodities orderCom = unitOfWork.OrderCommoditieses.Get(oc => oc.CommodityId == commodityId && oc.OrderId == orderId).First();
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
            var requiredInformation = unitOfWork.RequiredInformations.Get(ri => ri.OrderId == orderId).FirstOrDefault();
            var orderComs = unitOfWork.OrderCommoditieses.Get().Where(b => b.OrderId == orderId);
            var orderUser = unitOfWork.OrderUsers.GetById(orderId);

            List<Commodity> commodities = new List<Commodity>();
            StringBuilder stringBuilder = new StringBuilder();
            

            foreach (var it in orderComs)
            {
                commodities.Add(unitOfWork.Commodities.GetById(it.CommodityId));
            }
            //int Count = 5;
            var user = unitOfWork.Users.Get(u => u.Id == orderUser.UserId).FirstOrDefault();
            if (orderUser.IsConfirmed)
            {
                stringBuilder.AppendFormat("User Login: {0}</br>", user.UserName).AppendLine();
                stringBuilder.AppendFormat("Phone Number: {0}</br>", requiredInformation.PhoneNumber).AppendLine();
                stringBuilder.AppendFormat("User full name: {0}</br>", requiredInformation.FullName).AppendLine();
                stringBuilder.AppendFormat("Name -- Count -- Price</br>").AppendLine();
                foreach (var it in commodities)
                {
                    //var basketCom = unitOfWork.BasketCommoditieses.Get().Where(b => b.CommodityId == it.Id).FirstOrDefault();
                    stringBuilder.AppendFormat("{0}--{1}--{2};</br>", it.Name, 5, it.Price * 5).AppendLine();
                    //stringBuilder.AppendLine();    
                }
                stringBuilder.AppendFormat("City: {0}</br>", requiredInformation.City).AppendLine();
                stringBuilder.AppendFormat("Address Line1: {0}</br>", requiredInformation.AddressLine1).AppendLine();
                stringBuilder.AppendFormat("Address Line2: {0}</br>", requiredInformation.AddressLine2).AppendLine();
                stringBuilder.AppendFormat("Postal Code: {0}</br>", requiredInformation.PostalCode).AppendLine();
                stringBuilder.AppendFormat("Payment method: {0}</br>", requiredInformation.PaymentMethod).AppendLine();
                stringBuilder.AppendFormat("Shipping method: {0}</br>", requiredInformation.ShippingMethod).AppendLine();
                
                stringBuilder.AppendFormat("Date: {0}</br>", orderUser.DataConfirmed);
            }

            Receipt receipt = new Receipt()
            {   RequiredInformationId = requiredInformation.Id,
                DateCheck = orderUser.DataConfirmed,
                Description = stringBuilder.ToString(),
                UserId = orderUser.UserId,
                ReceiptCommoditieses = new List<ReceiptCommodities>()
                {

                }
                
            };
            unitOfWork.Receipts.Insert(receipt);

            foreach (var item2 in orderComs)
            {
                var newRecCom = new ReceiptCommodities()
                {
                    CommodityId = item2.CommodityId,
                    Receipt = receipt,
                    Amount = item2.Amount

                };

                unitOfWork.ReceiptCommoditieses.Insert(newRecCom);
            }

            

            string mail = receipt.Description;
            
            EmailSender emailSender =new EmailSender();
           
                emailSender.SendEmail(user.Email, "Y-Team Store", $"You check :\n{mail}");
            

         
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

        public void AddNewOrder(RequiredInformationViewModel recInfo)
        {
            var basket = unitOfWork.Baskets.GetById(recInfo.BasketId);
            if (basket == null) { return; }

            var commodities = unitOfWork.BasketCommoditieses.Get(b => b.BasketId == recInfo.BasketId);
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
                    OrderUser = order,
                    Amount = unitOfWork.BasketCommoditieses.Get(bc => bc.CommodityId == item.CommodityId).First().Amount
                    
                };

                unitOfWork.OrderCommoditieses.Insert(newOrderCom);
            }

            unitOfWork.Save();

            var neworder = unitOfWork.OrderUsers.Get(o => o.DataOrder == order.DataOrder&&o.IsConfirmed==order.IsConfirmed).First();

            var reqInformation = mapper.Map<RequiredInformationViewModel, RequiredInformation>(recInfo);
            reqInformation.OrderId = neworder.Id;
            unitOfWork.RequiredInformations.Insert(reqInformation);
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
