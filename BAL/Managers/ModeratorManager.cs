using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.ModeratorViewModels;
using System;
using System.Collections.Generic;
using System.Text;
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
            OrderUser moder = unitOfWork.OrderUsers.GetById(id);

            return mapper.Map<OrderUser, ModeratorViewModel>(moder);
        }

        public IEnumerable<ModeratorViewModel> GetModerators()
        {
            IEnumerable<Moderator> moders = unitOfWork.Moderators.GetAll();

            return mapper.Map<IEnumerable<Moderator>, List<ModeratorViewModel>>(moders);
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
