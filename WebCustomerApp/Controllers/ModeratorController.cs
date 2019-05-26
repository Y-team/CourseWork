using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BAL.Interfaces;
using BAL.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.ViewModels.GroupViewModels;
using Model.ViewModels.ModeratorViewModels;
using Model.ViewModels.UserViewModels;
using WebCustomerApp.Models;
using WebCustomerApp.Models.AccountViewModels;
using WebCustomerApp.Services;

namespace WebApp.Controllers
{ 
        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]")]
        public class ModeratorController : Controller
        {
        private readonly IModeratorManager moderatorManager;
        private readonly UserManager<ApplicationUser> userManager;
        public ModeratorController(UserManager<ApplicationUser> _userManager, IModeratorManager moderatorManager)
        {
            this.moderatorManager = moderatorManager;
            this.userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(moderatorManager.GetModerators());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blockedUser = moderatorManager.GetById(id);

            if (blockedUser == null)
            {
                return NotFound();
            }
            return View(blockedUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ModeratorViewModel moder)
        {

            if (ModelState.IsValid)
            {
                moderatorManager.Update(moder);
                return RedirectToAction("Index");
            }
            return View(moder);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var moderator = moderatorManager.GetById(id);

            if (moderator == null)
            {
                return NotFound();
            }
            return View(moderator);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            moderatorManager.Delete(id);
            return RedirectToAction("Index" ,"Moderator");
        }

        [HttpGet]
        public IEnumerable<ModeratorViewModel> GetAll()
        {
            IEnumerable<ModeratorViewModel> moderators = moderatorManager.GetModerators();
            return moderators;
        }


        [HttpPost]
        public async Task<IActionResult> Create(ModeratorViewModel item)
        {
            if (ModelState.IsValid)
            {
              ApplicationUser user =  moderatorManager.GetUserByEmail(item.Email);
              if (user == null)
              {
                  return RedirectToAction("Create", "Moderator");
              }
                await userManager.RemoveFromRoleAsync(user, "Moderator");
                await userManager.AddToRoleAsync(user, "Moderator");

                item.UserId = user.Id;
              item.UserName = user.UserName;
              moderatorManager.Insert(item);
            }
            return RedirectToAction("Index", "Moderator");
        }
    }
}
