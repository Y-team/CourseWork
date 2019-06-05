using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.RequiredInformationViewModel;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class RequiredInormationManager : BaseManager, IRequiredInormationManager
    {

        public RequiredInormationManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public RequiredInformationViewModel Get(int id)
        {
            throw new NotImplementedException();
        }
        

        public void Insert(RequiredInformationViewModel item)
        {
            var reqInf= mapper.Map<RequiredInformationViewModel,RequiredInformation>(item);
            unitOfWork.RequiredInformations.Insert(reqInf);
            unitOfWork.Save();
        }

        public void Update(RequiredInformationViewModel item)
        {
            throw new NotImplementedException();
        }
    }
}
