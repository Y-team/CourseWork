using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.ReceiptViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCustomerApp.Models;

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
            return mapper.Map<IEnumerable<Receipt>, IEnumerable<ReceiptViewModel>>(receipts);
        }
        //public ReceiptViewModel CreateReceipt(int orderId)
        //{
        //    var orderComs = unitOfWork.OrderCommoditieses.Get().Where(b => b.OrderId == orderId);
        //    var orderUser = unitOfWork.OrderUsers.GetById(orderId);

        //    List<Commodity> commodities = new List<Commodity>();
        //    StringBuilder stringBuilder = new StringBuilder();

        //    foreach (var item in orderComs)
        //    {
        //        commodities.Add(unitOfWork.Commodities.GetById(item.CommodityId));
        //    }
        //    int Count = 5;   
            
        //    if (orderUser.IsConfirmed)
        //    {
        //        stringBuilder.AppendFormat("User: {0}", orderUser.User.UserName).AppendLine();
        //        stringBuilder.AppendFormat("Name -- Count -- Price").AppendLine();
        //        foreach (var item in commodities)
        //        {
        //            stringBuilder.AppendFormat("{0}--{1}--{2};", item.Name, Count,item.Price*Count).AppendLine();
        //            //stringBuilder.AppendLine();    
        //        }
        //        stringBuilder.AppendFormat("Date: {0}", orderUser.DataConfirmed);
        //    }

        //    Receipt receipt = new Receipt()
        //    {
        //        DateCheck = orderUser.DataConfirmed,
        //        Description = stringBuilder.ToString(),
        //        UserId = orderUser.UserId
        //    };



        //    unitOfWork.Receipts.Insert(receipt);
        //    unitOfWork.Save();
        //    unitOfWork.OrderUsers.Delete(orderUser);

        //    return mapper.Map<Receipt, ReceiptViewModel>(receipt);
        //}
    }
}
