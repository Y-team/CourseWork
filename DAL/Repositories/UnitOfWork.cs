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
        private IBaseRepository<Recipient> recipientRepo;
        private IBaseRepository<StopWord> stopWordRepo;
        private IBaseRepository<Company> companyRepo;
        private IBaseRepository<Operator> operatorRepo;
        private IContactRepository contactRepo;
        private IBaseRepository<Phone> phoneRepo;
        private IBaseRepository<Tariff> tariffRepo;
        private IBaseRepository<Code> codeRepo;
        private IBaseRepository<ApplicationGroup> groupRepo;
        private IBaseRepository<Basket> basketRepo;
        private IBaseRepository<BasketCommodities> basketCommoditiesRepo;
        private IBaseRepository<BlockedUser> blockedUserRepo;
        private IBaseRepository<Commodity> commodityRepo;
        private IBaseRepository<LongDescription> longdescriptionRepo;
        private IBaseRepository<Moderator> moderatorRepo;
        private IBaseRepository<OrderUser> orderUserRepo;
        private IBaseRepository<OrderCommodities> ordercomoditiesRepo;
        private IBaseRepository<Photo> photoRepo;

        private IMailingRepository mailingRepo;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        #region it was

        

       
        public IBaseRepository<Company> Companies
        {
            get
            {
                if (companyRepo == null) { companyRepo = new BaseRepository<Company>(context); }
                return companyRepo;
            }
        }

        public IBaseRepository<Recipient> Recipients {
            get {
                if (recipientRepo == null) { recipientRepo = new BaseRepository<Recipient>(context); }
                return recipientRepo;
            }
        }
        public IBaseRepository<Tariff> Tariffs
        {
            get
            {
                if (tariffRepo == null) { tariffRepo = new BaseRepository<Tariff>(context); }
                return tariffRepo;
            }
        }
      

        public IBaseRepository<StopWord> StopWords
        {
            get
            {
                if (stopWordRepo == null) { stopWordRepo = new BaseRepository<StopWord>(context); }
                return stopWordRepo;
            }
        }

        public IBaseRepository<Operator> Operators
        {
            get
            {
                if (operatorRepo == null)
                {
                    operatorRepo = new BaseRepository<Operator>(context);
                }
                return operatorRepo;
            }
        }

        public IContactRepository Contacts {
            get {
                if (contactRepo == null) { contactRepo = new ContactRepository(context); }
                return contactRepo;
            }
        }

        public IBaseRepository<Phone> Phones {
            get {
                if (phoneRepo == null) { phoneRepo = new BaseRepository<Phone>(context); }
                return phoneRepo;
            }
        }

        public IBaseRepository<ApplicationGroup> ApplicationGroups
        {
            get
            {
                if (groupRepo == null) { groupRepo = new BaseRepository<ApplicationGroup>(context); }
                return groupRepo;
            }
        }

        public IBaseRepository<Code> Codes
        {
            get
            {
                if (codeRepo == null)
                {
                    codeRepo = new BaseRepository<Code>(context);
                }
                return codeRepo;
            }
        }

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

        public IMailingRepository Mailings
        {
            get
            {
                if (mailingRepo == null)
                {
                    mailingRepo = new MailingRepository(context);
                }
                return mailingRepo;
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
