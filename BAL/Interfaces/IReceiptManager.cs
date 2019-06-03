using System;
using System.Collections.Generic;
using System.Text;
using Model.ViewModels.ReceiptViewModels;

namespace BAL.Interfaces
{
    public interface IReceiptManager
    {
        ReceiptViewModel Get(int id);
        void Insert(ReceiptViewModel item);
        void Update(ReceiptViewModel item);
        void Delete(int id);
        //ReceiptViewModel CreateReceipt(int orderId);
        IEnumerable<ReceiptViewModel> ShowAllReceipts(string UserId);
    }
}
