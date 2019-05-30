using Model.ViewModels.BasketViewModels;
using Model.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Interfaces
{
    public interface IBasketManager
    {
        BasketViewModel Get(int id);
        BasketViewModel GetBusket(string userId, bool authorization);
        void Insert(string userId);
        void Update(BasketViewModel item);
        void Delete(int id);
        IEnumerable<CommodityUserViewModel> ShowCommodity(string userId);
        void CreateBasket(string userId);
    }
}
