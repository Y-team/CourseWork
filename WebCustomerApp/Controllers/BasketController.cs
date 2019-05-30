using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.BasketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCustomerApp.Models;

namespace WebApp.Controllers
{
   
    [Route("[controller]/[action]")]
    public class BasketController:Controller
    {
        private readonly IBasketManager basketManager;
        private readonly IBasketCommoditiesManager basketCommoditiesManager;
        private readonly UserManager<ApplicationUser> userManager;
        public BasketController(IBasketManager basketManager, IBasketCommoditiesManager basketCommoditiesManager, UserManager<ApplicationUser> userManager)
        {
            this.basketManager = basketManager;
            this.basketCommoditiesManager = basketCommoditiesManager;
            this.userManager = userManager;
        }

        public IActionResult Delete(int busketId, int commodityId)
        {
            basketCommoditiesManager.Delete(busketId, commodityId);
            return RedirectToAction("Index", "Basket");
        }
        [HttpGet]
        public IActionResult Index()
        {
            BasketCommoditiesUserViewModel basketU = new BasketCommoditiesUserViewModel();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var basket = basketManager.GetBusket(userId, User.Identity.IsAuthenticated);
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
            basketCommoditiesManager.Clean(basketId);
            return RedirectToAction("Index", "Basket");
        }
    }
}
