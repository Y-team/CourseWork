using Model.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface ICommodityManager
    {
        CommodityViewModel Get(int id);
        IEnumerable<CommodityViewModel> GetCommodities();
        IEnumerable<CommodityViewModel> GetModeratorCommodities(int moderatorId);
        IEnumerable<CommodityUserViewModel> GetUserCommodities();
        void Insert(CommodityViewModel item);
        void Update(CommodityViewModel item);
        void Delete(int id);

    }
}
