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
    [Authorize(Roles = "Moderator")]
    [Route("[controller]/[action]")]
    public class CommodityController:Controller
    {
        private readonly ICommodityManager commodityManager;

        public CommodityController(ICommodityManager commodityManager)
        {
            this.commodityManager = commodityManager;
        }
        [HttpPost]
        public IActionResult AddCommodity(CommodityViewModel item)
        {
            commodityManager.Insert(item);
            return RedirectToAction("Index", "Commodity");
        }
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
