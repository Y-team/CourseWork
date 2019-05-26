using BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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


    }
}
