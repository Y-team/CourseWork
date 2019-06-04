using Model.ViewModels.BlokedUserBiewModels;
using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Interfaces
{
    public interface IBlockedUserManager
    {
        // BlockedUserViewModel Get(int id);

        IEnumerable<BlockedUserViewModel> GetBlockedUsers();
        ApplicationUser GetUserByEmail(string email);
        BlockedUserViewModel GetById(int id);
        void Insert(BlockedUserViewModel item);
        void Update(BlockedUserViewModel item);
        void Delete(int id);
    }
}
