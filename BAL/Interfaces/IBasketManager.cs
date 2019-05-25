using Model.ViewModels.BasketViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface IBasketManager
    {
        BasketViewModel Get(int id);
        IEnumerable<BasketViewModel> GetBuskets(string userId);
        void Insert(BasketViewModel item);
        void Update(BasketViewModel item);
        void Delete(int id);
    }
}
