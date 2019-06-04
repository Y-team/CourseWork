using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.OrderCommodityViewModels;
using Model.ViewModels.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCustomerApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class OrderUserController:Controller
    {

        private readonly IOrderUserManager orderUserManager;
        private readonly IOrderCommoditiesManager orderCommoditiesManager;
        private readonly IModeratorManager moderatorManager;
        private readonly ICommodityManager commodityManager;

        public OrderUserController(
            IOrderUserManager orderUserManager, 
            IOrderCommoditiesManager orderCommoditiesManager, 
            IModeratorManager moderatorManager, 
            ICommodityManager commodityManager)
        {
            this.orderUserManager = orderUserManager;
            this.orderCommoditiesManager = orderCommoditiesManager;
            this.moderatorManager = moderatorManager;
            this.commodityManager = commodityManager;
        }



        public IActionResult Index()
        {

            var moderatorId = moderatorManager.GetThisModerator(this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;

            var item = orderCommoditiesManager.ShowAllOrderForModer(moderatorId);

            return View(item);
        }
        public IActionResult UnConfirmed()
        {
            var moderatorId = moderatorManager.GetThisModerator(this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;

            var item = orderCommoditiesManager.ShowOrderForModerUnAccepted(moderatorId);

            return View(item);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
       public IActionResult Delete(int commodityId, int orderId)
        {
            orderCommoditiesManager.Delete(commodityId,orderId);
            return RedirectToAction("Index", "OrderUser");
        }

        [HttpGet]
        public IActionResult Buy(OrderUserViewModel item)
        {
            item.DataOrder = DateTime.Now;
            item.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            orderUserManager.Update(item);
            return View(item);
        }

        [Authorize(Roles = "Moderator")]
        
        public IActionResult Confirm(int CommodityId,int OrderId)
        {

           orderCommoditiesManager.Update(orderCommoditiesManager.Confirmed(CommodityId,OrderId));

            return RedirectToAction("Index","OrderUser");
        }

       






    }
}
