using Model.ViewModels.CommodityViewModels;
using Model.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface ICommodityManager
    {
        CommodityViewModel Get(int id);
        IEnumerable<CommodityViewModel> GetCommodities();
        IEnumerable<CommodityUserViewModel> GetCommodities(int page, int countOnPage, string searchValue);
        IEnumerable<CommodityUserViewModel>GetCommodities(int ModeratorId, int page, int countOnPage, string searchValue);
        int GetCommodityCount(string searchValue);
        int GetCommodityCount(int moderatorId, string searchValue);
        IEnumerable<CommodityViewModel> GetModeratorCommodities(int moderatorId);

        IEnumerable<CommodityUserViewModel> GetUserCommodities();
        void Insert(CommodityViewModel item);
        void Update(CommodityViewModel item);
        void Delete(int id);
         

    }
}
