using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCustomerApp.Models;

namespace WebApp.Controllers
{
   
    [Route("[controller]/[action]")]
    public class CommodityController:Controller
    {
        private readonly ICommodityManager commodityManager;

        private readonly IModeratorManager moderatorManager;

        private readonly IBasketManager basketManager;

        private readonly IBasketCommoditiesManager basketCommoditiesManager;

        

        public CommodityController(ICommodityManager commodityManager, 
            IModeratorManager moderatorManager, 
            IBasketManager basketManager,
            IBasketCommoditiesManager basketCommoditiesManager)
        {
            this.commodityManager = commodityManager;
            this.moderatorManager = moderatorManager;
            this.basketManager = basketManager;
            this.basketCommoditiesManager = basketCommoditiesManager;
        }
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IActionResult AddCommodity(CommodityViewModel item)
        {

            item.ModeratorId =moderatorManager.GetThisModerator(this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;
          
            commodityManager.Insert(item);
            return RedirectToAction("Index", "Commodity");
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult AddCommodity()
        {
            return View();
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public IActionResult DeleteConfirmed(int commodityId)
        {

            commodityManager.Delete(commodityId);
            return RedirectToAction("Index", "Commodity");
        }
        [Authorize(Roles = "Moderator,Admin")]
        [HttpPost]
        public IActionResult Delete(int commodityId)
        {
            var commodity = commodityManager.Get(commodityId);

            if (commodity == null)
            {
                return NotFound();
            }
            return View(commodity);
        }
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IActionResult Edit(CommodityViewModel item)
        {

            if (ModelState.IsValid)
            {
                commodityManager.Update(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult Edit(int commodityId)
        {
            var commodity = commodityManager.Get(commodityId);

            if (commodity == null)
            {
                return NotFound();
            }
            return View(commodity);
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
               return View(commodityManager.GetCommodities());
            }
            var coms = commodityManager.GetModeratorCommodities(moderatorManager.GetThisModerator(
                       this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Id);
            return View(coms);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult IndexAdmin()
        {
            return View(commodityManager.GetCommodities());
        }

        [Authorize(Roles = "Moderator,Admin")]
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult Details(int commodityId)
        {
            return View(commodityManager.Get(commodityId));
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public IActionResult ShowCommodities()
        {
            var comms= commodityManager.GetUserCommodities();
            return View(comms);
        }
      [Authorize]
      public IActionResult AddToBasket(int commodityId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bask = basketManager.GetBusket(userId, User.Identity.IsAuthenticated);

            basketCommoditiesManager.Create(bask.Id, commodityId);

            return RedirectToAction("ShowCommodities", "Commodity");
        }

       

        public int GetCommoditiesCount(string searchValue)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }
            if (!User.Identity.IsAuthenticated)
            {
                return 0;
            }
            return commodityManager.GetCommodityCount(searchValue);
        }

        public IEnumerable<CommodityUserViewModel> Get(int page, int countOnPage, string searchValue)
        {
            if (searchValue == null)
            {
                searchValue = "";
            }

            if (User.Identity.IsAuthenticated && User.IsInRole("Moderator"))
            {
                var moderatorId = moderatorManager.GetThisModerator(this.User.FindFirstValue(ClaimTypes.NameIdentifier))
                    .Id;
                return commodityManager.GetCommodities(moderatorId, page, countOnPage, searchValue);
            }
            else
            {
              return commodityManager.GetCommodities(page, countOnPage, searchValue);
            }
        }

       
    }
}
