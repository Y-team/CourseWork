using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.BasketViewModels;
using Model.ViewModels.CommodityViewModels;
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

        public void DeleteBC(int id)
        {
            Basket bask = unitOfWork.Baskets.GetById(id);
            unitOfWork.Baskets.Delete(bask);
            unitOfWork.Save();
        }

       

        public BasketViewModel Get(int id)
        {
            Basket bask = unitOfWork.Baskets.GetById(id);
            
            return mapper.Map<Basket, BasketViewModel>(bask);
        }

        public BasketCommoditiesUserViewModel GetBasket(string userId,bool authorization)
        {

            Basket bask = unitOfWork.Baskets.Get(b => b.UserId == userId).FirstOrDefault();
            var user = unitOfWork.Users.GetAll().Where(u => u.Id == userId).FirstOrDefault();

            if (bask == null && authorization && user!=null)
            { 
            user.Basket = new Basket() { Description = "new Basket" };

                bask = unitOfWork.Baskets.Get(b => b.UserId == userId).FirstOrDefault();
                unitOfWork.Save();
            }
            return mapper.Map<Basket, BasketCommoditiesUserViewModel>(bask);
            
           
        }

        public void Insert(string userId)
        {
            Basket bask = new Basket()
            {
                UserId = userId
            };
            unitOfWork.Baskets.Insert(bask);
            unitOfWork.Save();
        }

       

        public void CreateBasket(string userId)
        {
            
                Basket bask = new Basket()
                {
                    UserId = userId
                };
            unitOfWork.Baskets.Insert(bask);
            unitOfWork.Save();
            
        }
        public void Update(BasketViewModel item)
        {
            Basket bask = mapper.Map<BasketViewModel, Basket>(item);
            unitOfWork.Baskets.Update(bask);
            unitOfWork.Save();
        }

        public IEnumerable<CommodityBasketViewModel> ShowCommodity(string userId)
        {
            var bask = unitOfWork.Baskets.Get(b => b.UserId == userId).FirstOrDefault();


            var basketComs = unitOfWork.BasketCommoditieses.GetAll().Where(b => b.BasketId == bask.Id).ToList();
            List<Commodity> commodities = new List<Commodity>();
            foreach (var item in basketComs)
            {
                var com = unitOfWork.Commodities.GetById(item.CommodityId);
                if (com != null)
                {
                    commodities.Add(com);
                }
            }

            var viewComms= mapper.Map<IEnumerable<Commodity>, List<CommodityBasketViewModel>>(commodities);

            for (int i=0;i<basketComs.Count();i++)
           {
               viewComms[i].Amount = basketComs[i].Amount;
           }

            return viewComms;
        }
    }
}
