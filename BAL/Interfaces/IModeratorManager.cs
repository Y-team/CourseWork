using System;
using System.Collections.Generic;
using System.Text;
using Model.ViewModels.ModeratorViewModels;

namespace BAL.Interfaces
{
    public interface IModeratorManager
    {
       // ModeratorViewModel Get(int id);
        IEnumerable<ModeratorViewModel> GetOrders();
        ModeratorViewModel GetById(int id);
        void Insert(ModeratorViewModel item);
        void Update(ModeratorViewModel item);
        void Delete(int id);
    }
}
