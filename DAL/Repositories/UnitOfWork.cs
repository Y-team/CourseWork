using WebCustomerApp.Models;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Data;
using Microsoft.AspNetCore.Identity;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
    
        private IBaseRepository<ApplicationUser> usersRepo;
        private IBaseRepository<Basket> basketRepo;
        private IBaseRepository<BasketCommodities> basketCommoditiesRepo;
        private IBaseRepository<BlockedUser> blockedUserRepo;
        private IBaseRepository<Commodity> commodityRepo;
        private IBaseRepository<LongDescription> longdescriptionRepo;
        private IBaseRepository<Moderator> moderatorRepo;
        private IBaseRepository<OrderUser> orderUserRepo;
        private IBaseRepository<OrderCommodities> ordercomoditiesRepo;
        private IBaseRepository<Photo> photoRepo;


        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region it was

        
        public IBaseRepository<Basket> Baskets
        {
            get
            {
                if (basketRepo == null) { basketRepo = new BaseRepository<Basket>(context); }
                return basketRepo;
            }
        }

        public IBaseRepository<BasketCommodities> BasketCommoditieses
        {
            get
            {
                if (basketCommoditiesRepo == null) { basketCommoditiesRepo = new BaseRepository<BasketCommodities>(context); }
                return basketCommoditiesRepo;
            }
        }

        public IBaseRepository<BlockedUser> BlockedUsers
        {
            get
            {
                if (blockedUserRepo == null) { blockedUserRepo = new BaseRepository<BlockedUser>(context); }
                return blockedUserRepo;
            }
        }

        public IBaseRepository<Commodity> Commodities
        {
            get
            {
                if (commodityRepo == null) { commodityRepo = new BaseRepository<Commodity>(context); }
                return commodityRepo;
            }
        }

        public IBaseRepository<LongDescription> LongDescriptions
        {
            get
            {
                if (longdescriptionRepo == null) { longdescriptionRepo = new BaseRepository<LongDescription>(context); }
                return longdescriptionRepo;
            }
        }

        public IBaseRepository<Moderator> Moderators
        {
            get
            {
                if (moderatorRepo == null) { moderatorRepo = new BaseRepository<Moderator>(context); }

                return moderatorRepo;
            }
        }

        public IBaseRepository<OrderUser> OrderUsers
        {
            get
            {
                if (orderUserRepo == null) { orderUserRepo = new BaseRepository<OrderUser>(context); }
                return orderUserRepo;
            }
        }
        public IBaseRepository<ApplicationUser> Users
        {
            get
            {
                if (usersRepo == null) { usersRepo = new BaseRepository<ApplicationUser>(context); }
                return usersRepo;
            }
        }
        public IBaseRepository<OrderCommodities> OrderCommoditieses
        {
            get
            {
                if (ordercomoditiesRepo == null) { ordercomoditiesRepo = new BaseRepository<OrderCommodities>(context); }
                return ordercomoditiesRepo;
            }
        }

        public IBaseRepository<Photo> Photoes
        {
            get
            {
                if (photoRepo == null) { photoRepo = new BaseRepository<Photo>(context); }
                return photoRepo;
            }
        }

     
        #endregion
        public int Save()
        {
            return context.SaveChanges();
        }

        private bool isDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                context.Dispose();
            }
            isDisposed = true;
        }
    }
}
