using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using Model.ViewModels.RequiredInformationViewModel;

namespace WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class RequiredInormationController : Controller
    {
        private readonly IRequiredInormationManager requiredInormationManager;
        private readonly IBasketCommoditiesManager basketCommoditiesManager;
        private readonly IOrderCommoditiesManager orderCommoditiesManager;
        public RequiredInormationController(IRequiredInormationManager requiredInormationManager, 
            IOrderCommoditiesManager orderCommoditiesManager, IBasketCommoditiesManager _basketCommoditiesManager)
        {
            this.orderCommoditiesManager = orderCommoditiesManager;
            this.requiredInormationManager = requiredInormationManager;
            this.basketCommoditiesManager = _basketCommoditiesManager;
        }

        [HttpGet]
        public IActionResult Create(int basketId)
        {
            RequiredInformationViewModel item=new RequiredInformationViewModel(){BasketId = basketId};
            return View(item);
        }
        [HttpPost]
        public IActionResult Create(RequiredInformationViewModel item )
        {
            int basketCom = basketCommoditiesManager.GetBasketCommodities(item.BasketId).Count();
            if (basketCom >= 1)
            {
              
                orderCommoditiesManager.AddNewOrder(item);
                ViewData["basketId"] = item.BasketId;
                basketCommoditiesManager.Clean(item.BasketId);

                return RedirectToAction("Index", "Basket");
            }

            return RedirectToAction("Index", "Basket");

            
        }
    }
}
