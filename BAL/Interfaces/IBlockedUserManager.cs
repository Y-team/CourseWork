using Model.ViewModels.BlokedUserBiewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface IBlockedUserManager
    {
        BlockedUserViewModel Get(int id);


    }
}
