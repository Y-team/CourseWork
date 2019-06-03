﻿using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.ViewModels.BasketViewModels;
using Model.ViewModels.CommodityViewModels;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class BasketCommoditiesManager:BaseManager, IBasketCommoditiesManager
    {
        public BasketCommoditiesManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }


        public void Update(IEnumerable<CommodityBasketViewModel> basketComms, int basketId)
        {
            try
            {
                foreach (var item in basketComms)
                {
                    unitOfWork.BasketCommoditieses.Update(
                        new BasketCommodities()
                        {
                            CommodityId = item.Id,
                            BasketId = basketId,
                            Amount = item.Amount
                        });
                }
            }
            catch { return;}

            unitOfWork.Save();
        }

        public void Delete(int basketId, int commodityId)
        {
            var basketCom = unitOfWork.BasketCommoditieses.Get(bc=>bc.BasketId==basketId&&bc.CommodityId==commodityId).FirstOrDefault();
            unitOfWork.BasketCommoditieses.Delete(basketCom);
            unitOfWork.Save();
        }

        public void Clean(int basketId)
        {
            var basketComs = unitOfWork.BasketCommoditieses.GetAll().Where(bc => bc.BasketId == basketId );
            foreach (var item in basketComs)
            {
                unitOfWork.BasketCommoditieses.Delete(item);
            }
            unitOfWork.Save();
        }

        public void Create(int basketId, int commodityId)
        {
            BasketCommodities basketCommodities = new BasketCommodities() { BasketId = basketId,CommodityId = commodityId , Amount = 1};
            unitOfWork.BasketCommoditieses.Insert(basketCommodities);
            unitOfWork.Save();
        }

        public IEnumerable<BasketCommodities> GetBasketCommodities(int basketId)
        {
            var baskets = unitOfWork.BasketCommoditieses.Get().Where(b => b.BasketId == basketId);

            return baskets;
        }
    }
}
