using BAL.Interface;
using Microsoft.AspNetCore.Identity;
using Model.DB;
using Model.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using Model.Interfaces;
using WebCustomerApp.Data;
using WebCustomerApp.Models;

namespace BAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _DbContext;
        private bool disposed = false;
        public UserManager<ApplicationUser> UserRepository     { get; }
        public SignInManager<ApplicationUser> SignInRepository { get; }
        private IBaseRepository<PhoneRec> phoneRepository;
        private IBaseRepository<Message> messageRepository;

        public UnitOfWork(ApplicationDbContext connectionContext,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInRepository)
        {
            _DbContext = connectionContext;
            UserRepository = userManager;
            SignInRepository = signInRepository;
        }
        
       
       
       
        public IBaseRepository<Message> MessageRepository
        {
            get
            {
                if (messageRepository == null)
                {
                    messageRepository = new BaseRepository<Message>(_DbContext);
                }
                return messageRepository;
            }

        }

        public IBaseRepository<PhoneRec> PhoneRepository
        {
            get
            {
                if (phoneRepository == null)
                {
                    phoneRepository = new BaseRepository<PhoneRec>(_DbContext);
                }
                return phoneRepository;
            }

        }

        #region default Save & Dispose
        public void Save()
        {
            _DbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _DbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

      

        #endregion
    }
}
