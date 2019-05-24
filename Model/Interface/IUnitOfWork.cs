using Microsoft.AspNetCore.Identity;
using Model.DB;
using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Model.Interfaces;

namespace Model.Interface
{
     public interface IUnitOfWork : IDisposable
    {
       

        UserManager<ApplicationUser> UserRepository { get; }
        SignInManager<ApplicationUser> SignInRepository { get; }

        IBaseRepository<PhoneRec> PhoneRepository { get; }
        IBaseRepository<Message> MessageRepository { get; }

        void Save();
        //void Dispose();
    }
}

