using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Moderator")]
    [Route("[controller]/[action]")]
    public class CommodityController:Controller
    {
        private readonly ICommodityManager commodityManager;

        private readonly IModeratorManager moderatorManager;

        public CommodityController(ICommodityManager commodityManager, IModeratorManager moderatorManager)
        {
            this.commodityManager = commodityManager;
            this.moderatorManager = moderatorManager;
        }
        [HttpPost]
        public IActionResult AddCommodity(CommodityViewModel item)
        {

            item.ModeratorId =moderatorManager.GetThisModerator(this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Id;
          
            commodityManager.Insert(item);
            return RedirectToAction("Index", "Commodity");
        }
        [HttpGet]
        public IActionResult AddCommodity()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteConfirmed(int Id)
        {

            commodityManager.Delete(Id);
            return RedirectToAction("Index", "Commodity");
        }
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
        [HttpPost]
        public IActionResult Edit(CommodityViewModel item)
        {

            commodityManager.Update(item);
            return RedirectToAction("Index", "Commodity");
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        //public IActionResult Edit()
        //{

        //}

        public IActionResult Index()
        {
            var item = commodityManager.GetCommodities();
            return View(item);
        }
    }
}
