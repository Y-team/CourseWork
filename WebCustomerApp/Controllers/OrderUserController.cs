using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class OrderUserController:Controller
    {

        private readonly IOrderUserManager orderUserManager;
        public OrderUserController(IOrderUserManager orderUserManager)
        {
            this.orderUserManager = orderUserManager;
        }



        public IActionResult Index()
        {
            return View();
        }

       public IActionResult Delete(int id)
        {
            orderUserManager.Delete(id);
            return RedirectToAction("Index", "OrderUser");
        }

        public IActionResult Buy(OrderUserViewModel item)
        {
            item.DataOrder = DateTime.Now;
            item.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            orderUserManager.Update(item);
            return View(item);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult ConfirmFromModer(OrderUserViewModel item)
        {
            item.IsConfirmed = true;
            item.DataConfirmed = DateTime.Now;
            orderUserManager.Update(item);
            return View(item);
        }

       

    }
}
