using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using WebCustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.ViewModels.OrderViewModels;
using StackExchange.Redis;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class OrderManager: BaseManager, IOrderManager
    {
        public OrderManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public void Delete(int id)
        {
            OrderUser order = unitOfWork.OrderUsers.GetById(id);
            unitOfWork.OrderUsers.Delete(order);
            unitOfWork.Save();
        }


        public OrderUserViewModel GetById(int id)
        {
            OrderUser order = unitOfWork.OrderUsers.GetById(id);
         
            return mapper.Map<OrderUser, OrderUserViewModel>(order);
        }

        public IEnumerable<OrderUserViewModel> GetOrders(string userId)
        {
            IEnumerable<OrderUser> orders = unitOfWork.OrderUsers.GetAll().Where(o=>o.UserId==userId);
          
            return mapper.Map<IEnumerable<OrderUser>, List<OrderUserViewModel>>(orders);
        }


        public void Insert(OrderUserViewModel item)
        {
            OrderUser order = mapper.Map<OrderUserViewModel, OrderUser>(item);
         
            unitOfWork.OrderUsers.Insert(order);
            unitOfWork.Save();
        }

        public void Update(OrderUserViewModel item)
        {
            OrderUser order = mapper.Map<OrderUserViewModel, OrderUser>(item);
            
            unitOfWork.OrderUsers.Update(order);
            unitOfWork.Save();
        }
    }
}
