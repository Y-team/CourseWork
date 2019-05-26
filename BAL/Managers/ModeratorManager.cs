using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.ModeratorViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Model.ViewModels.UserViewModels;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class ModeratorManager: BaseManager, IModeratorManager
    {
        public ModeratorManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public void Delete(int id)
        {
            Moderator order = unitOfWork.Moderators.GetById(id);
            unitOfWork.Moderators.Delete(order);
            unitOfWork.Save();
        }


        public ModeratorViewModel GetById(int id)
        {
            Moderator moderator = unitOfWork.Moderators.GetById(id);
            var moder= mapper.Map<Moderator, ModeratorViewModel>(moderator);
            var timeU = unitOfWork.Users.Get(u => u.Id == moder.UserId).First();
            moder.Email = timeU.Email;
            moder.UserName = timeU.UserName;
            return moder;
        }
         public ApplicationUser GetUserByEmail(string email)
         {
             var moderator = unitOfWork.Users.GetAll().Where(u => u.Email == email).FirstOrDefault();
           
             return moderator;
        }
        public IEnumerable<ModeratorViewModel> GetModerators()
        {
            IEnumerable<Moderator> moders = unitOfWork.Moderators.GetAll();
            var modersView = mapper.Map<IEnumerable<Moderator>, List<ModeratorViewModel>>(moders);
            foreach (var moder in modersView)
            { var timeU = unitOfWork.Users.Get(u => u.Id == moder.UserId).First();
                moder.Email=timeU.Email;
                moder.UserName = timeU.UserName;
            }

            return modersView;
        }


        public void Insert(ModeratorViewModel item)
        {
            Moderator order = mapper.Map<ModeratorViewModel, Moderator>(item);

            unitOfWork.Moderators.Insert(order);
            unitOfWork.Save();
        }

        public void Update(ModeratorViewModel item)
        {
            Moderator moder = mapper.Map<ModeratorViewModel, Moderator>(item);

            unitOfWork.Moderators.Update(moder);
            unitOfWork.Save();
        }
    }
}
