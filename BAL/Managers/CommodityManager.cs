using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.CommodityViewModels;
using Model.ViewModels.ModeratorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class CommodityManager:BaseManager,ICommodityManager
    {
        public CommodityManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public void Delete(int id)
        {
            Commodity commodity = unitOfWork.Commodities.GetById(id);
            unitOfWork.Commodities.Delete(commodity);
            unitOfWork.Save();
        }

        public CommodityViewModel Get(int id)
        {
            Commodity commodity = unitOfWork.Commodities.GetById(id);

            return mapper.Map<Commodity, CommodityViewModel>(commodity);
        }

        public IEnumerable<CommodityViewModel> GetCommodities()
        {
            IEnumerable<Commodity> commodities = unitOfWork.Commodities.GetAll();
            return mapper.Map<IEnumerable<Commodity>, IEnumerable<CommodityViewModel>>(commodities);
        }

        public void Insert(CommodityViewModel item)
        {
            Commodity commodity = mapper.Map<CommodityViewModel, Commodity>(item);
            unitOfWork.Commodities.Insert(commodity);
            unitOfWork.Save();
        }

        public void Update(CommodityViewModel item)
        {
            Commodity commodity = mapper.Map<CommodityViewModel, Commodity>(item);
            unitOfWork.Commodities.Update(commodity);
            unitOfWork.Save();
        }
        
        public IEnumerable<CommodityViewModel> GetModeratorCommodities(int moderatorId)
        {
            
            IEnumerable<Commodity> commodities = unitOfWork.Commodities.Get(c=>c.ModeratorId==moderatorId);
            return mapper.Map<IEnumerable<Commodity>, IEnumerable<CommodityViewModel>>(commodities);
        }
    }
}
