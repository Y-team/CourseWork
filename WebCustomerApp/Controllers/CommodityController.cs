using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.CommodityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Moderator,Admin")]
    [Route("[controller]/[action]")]
    public class CommodityController:Controller
    {
        private readonly ICommodityManager commodityManager;

        public CommodityController(ICommodityManager commodityManager)
        {
            this.commodityManager = commodityManager;
        }
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IActionResult AddCommodity(CommodityViewModel item)
        {
            commodityManager.Insert(item);
            return RedirectToAction("Index", "Commodity");
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult AddCommodity()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int commodityId)
        {
            commodityManager.Delete(commodityId);
            return RedirectToAction("Index", "Commodity");
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IActionResult Edit(CommodityViewModel item)
        {
            commodityManager.Update(item);
            return RedirectToAction("Index", "Commodity");
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult Index()
        {
            string moderatorId = "";
            var coms = commodityManager.GetModeratorCommodities(moderatorId);
            return View(coms);
        }

        public IActionResult IndexAdmin()
        {
            return View();
        }
    }
}
