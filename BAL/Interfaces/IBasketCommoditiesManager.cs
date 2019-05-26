using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface IBasketCommoditiesManager
    {
        void Delete(int basketId, int commodityId);
        void Clean(int basketId);


        void Create(int basketId, int commodityId);
        
    }
}
