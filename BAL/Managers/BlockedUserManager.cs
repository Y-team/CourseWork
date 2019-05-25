using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.BlokedUserBiewModels;
using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class BlockedUserManager: BaseManager, IBlockedUserManager
    {
        public BlockedUserManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public BlockedUserViewModel Get(int id)
        {
            BlockedUser bu = unitOfWork.BlockedUsers.GetById(id);

            return mapper.Map<BlockedUser, BlockedUserViewModel>(bu);
        }
    }
}
