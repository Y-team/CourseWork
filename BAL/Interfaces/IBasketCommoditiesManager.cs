using Model.ViewModels.BasketViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Model.ViewModels.CommodityViewModels;

namespace BAL.Interfaces
{
    public interface IBasketCommoditiesManager
    {
        void Delete(int basketId, int commodityId);
        void Clean(int basketId);

        void Update(IEnumerable<CommodityBasketViewModel> basketComms,int basketId);
        void Create(int basketId, int commodityId);
        
    }
}
