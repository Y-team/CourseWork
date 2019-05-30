using WebCustomerApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Model.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<ApplicationUser> Users { get; }
        
        IBaseRepository<Basket> Baskets { get; }
        IBaseRepository<BasketCommodities> BasketCommoditieses { get; }
        IBaseRepository<LongDescription> LongDescriptions { get; }
        IBaseRepository<Commodity> Commodities { get; }
        IBaseRepository<Moderator> Moderators { get;  }
        IBaseRepository<Photo> Photoes { get; }
        IBaseRepository<BlockedUser> BlockedUsers { get; }
        IBaseRepository<OrderUser> OrderUsers { get; }
        IBaseRepository<OrderCommodities> OrderCommoditieses { get;  }
       


        int Save();
    }

}
