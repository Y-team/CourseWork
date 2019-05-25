using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.ModeratorViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Managers
{
    public class ModeratorManager: BaseManager, IModeratorManager
    {
        public ModeratorManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public void Delete(int id)
        {
            //Moderator commodity = unitOfWork.Commodities.GetById(id);
            //unitOfWork.Commodities.Delete(commodity);
            //unitOfWork.Save();
        }

        public ModeratorViewModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ModeratorViewModel> GetModerators()
        {
            throw new NotImplementedException();
        }

        public void Insert(ModeratorViewModel item)
        {
            throw new NotImplementedException();
        }

        public void Update(ModeratorViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
