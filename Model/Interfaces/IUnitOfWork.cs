using WebCustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Recipient> Recipients { get; }
        IContactRepository Contacts { get; }
        IBaseRepository<Phone> Phones { get; }
        IBaseRepository<Company> Companies { get; }
        IBaseRepository<Operator> Operators { get; }
        IBaseRepository<Code> Codes { get; }
        IBaseRepository<Tariff> Tariffs { get; }
        IBaseRepository<StopWord> StopWords { get; }

        IBaseRepository<Basket> Baskets { get; }
        IBaseRepository<BasketCommodities> BasketCommoditieses { get; }
        IBaseRepository<LongDescription> LongDescriptions { get; }
        IBaseRepository<Commodity> Commodities { get; }
        IBaseRepository<Moderator> Moderators { get;  }
        IBaseRepository<Photo> Photoes { get; }
        IBaseRepository<BlockedUser> BlockedUsers { get; }
        IBaseRepository<Order> Orders { get; }
        IBaseRepository<OrderCommodities> OrderCommoditieses { get;  }
        IMailingRepository Mailings { get; }


        IBaseRepository<ApplicationGroup> ApplicationGroups { get; }
     
        int Save();
    }

}
