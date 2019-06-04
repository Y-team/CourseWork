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
       
    }
}
