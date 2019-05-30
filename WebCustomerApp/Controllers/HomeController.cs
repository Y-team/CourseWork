using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCustomerApp.Models;

namespace WebCustomerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBasketManager basketManager;

        public HomeController(IBasketManager basketManager)
        {
            this.basketManager = basketManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) { 
            basketManager.GetBusket(this.User.FindFirstValue(ClaimTypes.NameIdentifier), User.Identity.IsAuthenticated);
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}