using System;
using System.Collections.Generic;
using System.Text;
using Model.ViewModels.RequiredInformationViewModel;

namespace BAL.Interfaces
{
   public interface IRequiredInormationManager
   {
       RequiredInformationViewModel Get(int id);
       void Insert(RequiredInformationViewModel item);
       void Update(RequiredInformationViewModel item);
       void Delete(int id);
    }
}
