using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.LongDescriptionViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class LongDescriptionManager: BaseManager, ILongDescriptionManager
    {
        public LongDescriptionManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public void Delete(int id)
        {
            LongDescription ld = unitOfWork.LongDescriptions.GetById(id);
            unitOfWork.LongDescriptions.Delete(ld);
            unitOfWork.Save();
        }

        public LongDescriptionViewModel GetById(int id)
        {
            LongDescription ld = unitOfWork.LongDescriptions.GetById(id);

            return mapper.Map<LongDescription, LongDescriptionViewModel>(ld);
        }

        public void Insert(LongDescriptionViewModel item)
        {
            LongDescription ld = mapper.Map<LongDescriptionViewModel, LongDescription>(item);
            unitOfWork.LongDescriptions.Insert(ld);
            unitOfWork.Save();
        }

        public void Update(LongDescriptionViewModel item)
        {
            LongDescription ld = mapper.Map<LongDescriptionViewModel, LongDescription>(item);
            unitOfWork.LongDescriptions.Update(ld);
            unitOfWork.Save();
        }
    }
}
