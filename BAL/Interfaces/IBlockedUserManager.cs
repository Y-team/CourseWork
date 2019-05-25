using Model.ViewModels.BlokedUserBiewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface IBlockedUserManager
    {
        // BlockedUserViewModel Get(int id);

        IEnumerable<BlockedUserViewModel> GetBlockedUsers();
        BlockedUserViewModel GetById(int id);
        void Insert(BlockedUserViewModel item);
        void Update(BlockedUserViewModel item);
        void Delete(int id);
    }
}
