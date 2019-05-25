using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Interfaces;
using BAL.Managers;
using Model.ViewModels.BlokedUserBiewModels;

namespace WebApp.Controllers
{
        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]")]
        public class BlockedUserController : Controller
        {
            private readonly  IBlockedUserManager blockedUserManager;

            public BlockedUserController(IBlockedUserManager blockedUser)
            {
                this.blockedUserManager = blockedUser;
            }

        [HttpGet]
        public IActionResult Index()
        {
            return View(blockedUserManager.GetBlockedUsers());
        }

        [HttpGet]
        public IActionResult Create()
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
        public IActionResult Create(BlockedUserViewModel item)
        {
            if (ModelState.IsValid)
            {
                blockedUserManager.Insert(item);
            }


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
