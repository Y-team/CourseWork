using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.Managers
{
    public class PhotoManager:BaseManager,IPhotoManager
    {
        public PhotoManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
