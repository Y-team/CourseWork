using System;
using System.Collections.Generic;
using System.Text;
using Model.ViewModels.OrderViewModels;

namespace BAL.Interfaces
{
    public interface IOrderManager
    {
        //OrderViewModel Get();
        IEnumerable<OrderUserViewModel> GetOrders(string userId);
        OrderUserViewModel GetById(int id);
        void Insert(OrderUserViewModel item);
        void Update(OrderUserViewModel item);
        void Delete(int id);
    }
}
