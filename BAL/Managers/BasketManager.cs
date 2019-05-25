using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.BasketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class BasketManager: BaseManager, IBasketManager
    {
        public BasketManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public void Delete(int id)
        {
            Basket bask = unitOfWork.Baskets.GetById(id);
            unitOfWork.Baskets.Delete(bask);
            unitOfWork.Save();
        }

        public void Delete(int basketId, int commodityId)
        {
            var baskcom = unitOfWork.BasketCommoditieses.Get(bc => bc.BasketId == basketId && bc.CommodityId == commodityId).First();
            unitOfWork.BasketCommoditieses.Delete(baskcom);
            unitOfWork.Save();
        }

        public BasketViewModel Get(int id)
        {
            Basket bask = unitOfWork.Baskets.GetById(id);
            
            return mapper.Map<Basket, BasketViewModel>(bask);
        }

        public IEnumerable<BasketViewModel> GetBuskets(string userId)
        {
            IEnumerable<Basket> bask = unitOfWork.Baskets.GetAll().Where(b=>b.ApplicationUser.Id==userId);
            return mapper.Map<IEnumerable<Basket>, IEnumerable<BasketViewModel>>(bask);
        }

        public void Insert(BasketViewModel item)
        {
            Basket bask = mapper.Map<BasketViewModel, Basket>(item);
            unitOfWork.Baskets.Insert(bask);
            unitOfWork.Save();
        }

        public void Update(BasketViewModel item)
        {
            Basket bask = mapper.Map<BasketViewModel, Basket>(item);
            unitOfWork.Baskets.Update(bask);
            unitOfWork.Save();
        }
    }
}
