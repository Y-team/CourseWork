using BAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebCustomerApp.Models;

namespace WebApp.Controllers
{
    public class PhotoController:Controller
    {
        private readonly IPhotoManager photoManager;

        public IActionResult Create()
        {
            return View();
        }

        
       

    }
}
