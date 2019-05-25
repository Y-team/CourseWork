using Model.ViewModels.ModeratorViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface IModeratorManager
    {
        ModeratorViewModel Get(int id);
        IEnumerable<ModeratorViewModel> GetModerators();
        void Insert(ModeratorViewModel item);
        void Update(ModeratorViewModel item);
        void Delete(int id);
    }
}
