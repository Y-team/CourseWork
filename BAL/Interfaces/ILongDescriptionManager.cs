using Model.ViewModels.LongDescriptionViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Interfaces
{
    public interface ILongDescriptionManager
    {
        LongDescriptionViewModel GetById(int id);   
        void Insert(LongDescriptionViewModel item);
        void Update(LongDescriptionViewModel item);
        void Delete(int id);
    }
}
