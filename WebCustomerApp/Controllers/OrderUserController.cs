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



            return View(orderCommoditiesManager.ShowAllOrderForModer(moderatorId));
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
       public IActionResult Delete(int id)
        {
            orderUserManager.Delete(id);
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
        
        public void ConfirmFromModer(OrderCommodityViewModel item)
        {
            item.IsConfirmeds = true;

            orderCommoditiesManager.Update(item);

            RedirectToAction("Index","OrderUser");
        }

       






    }
}
