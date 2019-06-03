using BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ReceiptController:Controller
    {
        private readonly IReceiptManager receiptManager;
        private readonly IOrderCommoditiesManager orderCommoditiesManager;
        private readonly IOrderUserManager orderUserManager;

        public ReceiptController(
            IReceiptManager receiptManager,
            IOrderCommoditiesManager orderCommoditiesManager,
            IOrderUserManager orderUserManager
            )
        {
            this.receiptManager = receiptManager;
            this.orderCommoditiesManager = orderCommoditiesManager;
            this.orderUserManager = orderUserManager;
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var receipts = receiptManager.ShowAllReceipts(userId);
            return View(receipts);
        }

       
    }
}
