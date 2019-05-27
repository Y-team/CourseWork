using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using Model.ViewModels.PhotoViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin,Moderator")]
    [Route("[controller]/[action]")]

    public class PhotoController : Controller
    {
        private readonly IPhotoManager photoManager;

        public PhotoController(IPhotoManager photoManager)
        {
            this.photoManager = photoManager;
        }

        [HttpGet]
        public IActionResult AddPhoto(int commodityId)
        {
            return View(new ImageViewModel() { CommodityId = commodityId });
        }

        [HttpPost]
        public IActionResult AddPhoto(ImageViewModel img)
        {
            if (ModelState.IsValid)
            {
                var result = photoManager.AddImage(img);
                if (!result.Success)
                {
                    TempData["ErrorMessage"] = result.Details;
                    return RedirectToAction("Details", "Commodity");
                }
                else
                {
                    return RedirectToAction("Details", "Commodity",new{img.CommodityId});
                }
            }
            TempData["ErrorMessage"] = "Internal error";
            return RedirectToAction("Operators", "Operator");
        }
    }
}
