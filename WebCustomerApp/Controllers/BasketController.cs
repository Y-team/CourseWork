using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.BasketViewModels;
using Model.ViewModels.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Model.ViewModels.CommodityViewModels;
using WebCustomerApp.Models;

namespace WebApp.Controllers
{
   
    [Route("[controller]/[action]")]
    public class BasketController:Controller
    {
        private readonly IBasketManager basketManager;
        private readonly IBasketCommoditiesManager basketCommoditiesManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderUserManager orderUserManager;
        private readonly IOrderCommoditiesManager orderCommoditiesManager;

        public BasketController(IBasketManager basketManager, 
            IBasketCommoditiesManager basketCommoditiesManager, 
            UserManager<ApplicationUser> userManager,
            IOrderUserManager orderUserManager,
            IOrderCommoditiesManager orderCommoditiesManager)
        {
            this.basketManager = basketManager;
            this.basketCommoditiesManager = basketCommoditiesManager;
            this.userManager = userManager;
            this.orderUserManager = orderUserManager;
            this.orderCommoditiesManager = orderCommoditiesManager;
        }

        public IActionResult Delete(int basketId, int commodityId)
        {
            ViewData["basketId"] = basketId;
            basketCommoditiesManager.Delete(basketId, commodityId);
            return RedirectToAction("Index", "Basket");
        }
        [HttpGet]
        public IActionResult Index()
        {
            BasketCommoditiesUserViewModel basketU = new BasketCommoditiesUserViewModel();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var basket = basketManager.GetBasket(userId, User.Identity.IsAuthenticated);
            if (basket != null)
            {
                basketU = new BasketCommoditiesUserViewModel()
                {
                    Id = basket.Id,
                    UserId = userId,
                    UserName = basket.UserName,
                    Description = basket.Description,
                };
                basketU.CommodityUser = basketManager.ShowCommodity(userId);
            }
            ViewData["basketId"] = basket.Id;
            if (this.User == null)
            {
                ApplicationUser appUser = new ApplicationUser() { UserName = "NonathorizedUser", Basket = new Basket() };
                userManager.CreateAsync(appUser);
                return View(basketManager.ShowCommodity(appUser.Id));
            }
            else
            {
                var item = basketManager.ShowCommodity(userId);
            }

            return View(basketU);
        }

        public IActionResult Clean(int basketId)
        {
            ViewData["basketId"] = basketId;
            basketCommoditiesManager.Clean(basketId);
            return RedirectToAction("Index", "Basket",new { basketId = basketId });
        }

        public IActionResult Confirm(int basketId)
        {
            int basketCom = basketCommoditiesManager.GetBasketCommodities(basketId).Count();
            if (basketCom >= 1)
            {
                orderCommoditiesManager.AddNewOrder(basketId);
                ViewData["basketId"] = basketId;
                basketCommoditiesManager.Clean(basketId);
            }

            return RedirectToAction("Index", "Basket");
        }
        public IActionResult Save(BasketCommoditiesUserViewModel basket)
        {
            IEnumerable<CommodityBasketViewModel> commodities = basket.CommodityUser;

         basketCommoditiesManager.Update(commodities,basket.Id);

            return RedirectToAction("Index", "Basket");
        }

    }
}
