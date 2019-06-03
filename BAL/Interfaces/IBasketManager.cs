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
        BasketCommoditiesUserViewModel GetBasket(string userId, bool authorization);
        void Insert(string userId);
        void Update(BasketViewModel item);
        void Delete(int id);
        IEnumerable<CommodityBasketViewModel> ShowCommodity(string userId);
        void CreateBasket(string userId);
    }
}
