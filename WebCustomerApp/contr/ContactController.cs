﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BAL.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels.ContactViewModels;
using WebCustomerApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactManager contactManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGroupManager groupManager;

        public ContactController(IContactManager contactManager, UserManager<ApplicationUser> userManager, IGroupManager groupManager)
        {
            this.contactManager = contactManager;
            this.groupManager = groupManager;
            this.userManager = userManager;
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [Route("~/Contact/GetContactList")]
        [HttpGet]
        public List<ContactViewModel> GetContactList(int pageNumber, int pageSize, string searchValue)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return null;
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                int groupId = userManager.Users.FirstOrDefault(u => u.Id == userId).ApplicationGroupId;
                if (searchValue == null)
                    return contactManager.GetContact(groupId, pageNumber, pageSize);
                else
                    return contactManager.GetContactBySearchValue(groupId, pageNumber, pageSize, searchValue);
            }
            catch
            {
                //throw ex;
                return null;
            }
        }

        [Route("~/Contact/GetContactCount")]
        [HttpGet]
        public int GetContactCount(string searchValue)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return 0;
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                int groupId = userManager.Users.FirstOrDefault(u => u.Id == userId).ApplicationGroupId;
                if (searchValue == null)
                    return contactManager.GetContactCount(groupId);
                else
                    return contactManager.GetContactBySearchValueCount(groupId, searchValue);
            }
            catch
            {
                //throw ex;
                return 0;
            }
        }

        [Route("~/Contact/AddContact")]
        [HttpPost]
        public IActionResult AddContact(ContactViewModel obj)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                int groupId = userManager.Users.FirstOrDefault(u => u.Id == userId).ApplicationGroupId;
                if (obj.Name == null)
                    obj.Name = "";
                if (obj.Surname == null)
                    obj.Surname = "";
                if (obj.Notes == null)
                    obj.Notes = "";
                if (obj.KeyWords == null)
                    obj.KeyWords = "";
                if (contactManager.CreateContact(obj, groupId))
                    return new ObjectResult("Phone added successfully!");
                else
                    return new ObjectResult("Contact with this phone number already exist!");
            }
            catch (Exception ex)
            {
                //throw ex;
                return new ObjectResult(ex.Message);
            }
        }

        [Route("~/Contact/DeleteContact/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                contactManager.DeleteContact(id);
                return new ObjectResult("Phone deleted successfully!");
            }
            catch (Exception ex)
            {
                //throw ex;
                return new ObjectResult(ex.Message);
            }
        }

        [Route("~/Contact/UpdateContact")]
        [HttpPut]
        public IActionResult UpdateContact(ContactViewModel obj)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                int groupId = userManager.Users.FirstOrDefault(u => u.Id == userId).ApplicationGroupId;
                if (obj.Name == null)
                    obj.Name = "";
                if (obj.Surname == null)
                    obj.Surname = "";
                if (obj.Notes == null)
                    obj.Notes = "";
                if (obj.KeyWords == null)
                    obj.KeyWords = "";
                contactManager.UpdateContact(obj, groupId);
                return new ObjectResult("Phone modified successfully!");
            }
            catch (Exception ex)
            {
                //throw ex;
                return new ObjectResult(ex.Message);
            }
        }

        [Route("~/Contact/GetContact/{id}")]
        [HttpGet]
        public ContactViewModel GetContact(int id)
        {
            try
            {
                ContactViewModel contact = contactManager.GetContact(id);
                return contact;
            }
            catch
            {
                //throw ex;
                return null;
            }
        }
    }
}
