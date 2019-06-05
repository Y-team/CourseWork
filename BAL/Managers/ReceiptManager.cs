using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.ReceiptViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.ViewModels.BasketViewModels;
using WebCustomerApp.Models;
using Model.ViewModels.CommodityViewModels;

namespace BAL.Managers
{
    public class ReceiptManager:BaseManager,IReceiptManager
    {
        public ReceiptManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public void Delete(int id)
        {
            Receipt receipt = unitOfWork.Receipts.GetById(id);
            unitOfWork.Receipts.Delete(receipt);
            unitOfWork.Save();
        }

        public ReceiptViewModel Get(int id)
        {
            Receipt receipt = unitOfWork.Receipts.GetById(id);
            return mapper.Map<Receipt, ReceiptViewModel>(receipt);
        }

        public void Insert(ReceiptViewModel item)
        {
            Receipt receipt = mapper.Map<ReceiptViewModel, Receipt>(item);
            unitOfWork.Receipts.Insert(receipt);
            unitOfWork.Save();
        }

        public void Update(ReceiptViewModel item)
        {
            Receipt receipt = mapper.Map<ReceiptViewModel, Receipt>(item);
            unitOfWork.Receipts.Update(receipt);
            unitOfWork.Save();
        }

        public IEnumerable<ReceiptViewModel> ShowAllReceipts(string UserId)
        {
            var receipts = unitOfWork.Receipts.Get().Where(r => r.UserId == UserId);
            var receiptsView= mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptViewModel>>(receipts);
          
            foreach (var item in receiptsView)
            {
                var reqInf = unitOfWork.RequiredInformations.GetById(item.RequiredInformationId);
                item.City = reqInf.City;
                item.AddressLine1 = reqInf.AddressLine1;
                item.AddressLine2 = reqInf.AddressLine2;
                item.FullName = reqInf.FullName;
                item.PaymentMethod = reqInf.PaymentMethod;
                item.PhoneNumber = reqInf.PhoneNumber;
                item.PostalCode = reqInf.PostalCode;
                item.ShippingMethod = reqInf.ShippingMethod;
                item.CommodityUser=new List<CommodityBasketViewModel>();
                var recCommodities = unitOfWork.ReceiptCommoditieses.Get(rc => rc.ReceiptId == item.Id);
                foreach (var citems in recCommodities)
                {
                    var com = unitOfWork.Commodities.GetById(citems.CommodityId); 
                   
                    var com2 = mapper.Map<Commodity, CommodityBasketViewModel>(com);
                    com2.Amount = citems.Amount;
                    item.CommodityUser.Add(com2);

                }

                foreach (var citems in item.CommodityUser)
                {
                    citems.Price = citems.Price * citems.Amount;
                }
            }

            return receiptsView;
        }
       
    }
}
