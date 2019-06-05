using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using BAL.Managers;
using Model.ViewModels.BlokedUserBiewModels;
using WebCustomerApp.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]")]
        public class BlockedUserController : Controller
        {
            private readonly  IBlockedUserManager blockedUserManager;
            private readonly UserManager<ApplicationUser> userManager;
        public BlockedUserController(IBlockedUserManager blockedUser, UserManager<ApplicationUser> userManager)
            {
                this.blockedUserManager = blockedUser;
                this.userManager = userManager;
            }

        [HttpGet]
        public IActionResult Index()
        {
            return View(blockedUserManager.GetBlockedUsers());
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var blockedUser = blockedUserManager.GetById(id);

            if (blockedUser == null)
            {
                return NotFound();
            }
            return View(blockedUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlockedUserViewModel wordEdit)
        {

            if (ModelState.IsValid)
            {
                blockedUserManager.Update(wordEdit);
                return RedirectToAction("Index");
            }
            return View(wordEdit);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {


            var blockedUser = blockedUserManager.GetById(id);

            if (blockedUser == null)
            {
                return NotFound();
            }
            return View(blockedUser);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            blockedUserManager.Delete(id);
            return RedirectToAction("Index");
        }

        [Route("~/StopWord/GetAll")]
        [HttpGet]
        public IEnumerable<BlockedUserViewModel> GetAll()
        {
            IEnumerable<BlockedUserViewModel> blockedUser = blockedUserManager.GetBlockedUsers();
            return blockedUser;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAsync(BlockedUserViewModel item)
        {
            if (ModelState.IsValid)
            {
                blockedUserManager.Insert(item);
            }
            ApplicationUser user = blockedUserManager.GetUserByEmail(item.Email);
            if (user == null)
            {
                return RedirectToAction("Index", "BlockedUser");
            }

            if (user.Email == "Admin@gmail.com")
            {
                return RedirectToAction("Index", "BlockedUser");
            }
            await userManager.RemoveFromRoleAsync(user, "BlockedUser");
            await userManager.AddToRoleAsync(user, "BlockedUser");

            await userManager.UpdateAsync(user);

            return RedirectToAction("Index", "BlockedUser");
        }

        [HttpGet]
        public IActionResult StopWordDetails(int id)
        {
            BlockedUserViewModel word = blockedUserManager.GetById(id);

            if (word == null)
            {
                return NotFound();
            }
            return View(word);
        }
    }
    
}
