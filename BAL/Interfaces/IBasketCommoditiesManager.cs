using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Interfaces
{
    public interface IBasketCommoditiesManager
    {
        void Delete(int basketId, int commodityId);
        void Clean(int basketId);
        void Create(int basketId, int commodityId);
        IEnumerable<BasketCommodities> GetBasketCommodities (int basketId);
    }
}
