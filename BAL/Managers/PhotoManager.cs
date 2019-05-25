using AutoMapper;
using BAL.Interfaces;
using Model.Interfaces;
using Model.ViewModels.PhoneViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using WebCustomerApp.Models;

namespace BAL.Managers
{
    public class PhotoManager:BaseManager,IPhotoManager
    {
        public PhotoManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public void Delete(int id)
        {
            Photo photo = unitOfWork.Photoes.GetById(id);
            unitOfWork.Photoes.Delete(photo);
            unitOfWork.Save();
        }

        public PhotoViewModel GetById(int id)
        {
            Photo photo = unitOfWork.Photoes.GetById(id);

            return mapper.Map<Photo, PhotoViewModel>(photo);
        }

        public IEnumerable<PhotoViewModel> GetAllPhotos()
        {
            IEnumerable<Photo> photos = unitOfWork.Photoes.GetAll();

             return mapper.Map<IEnumerable<Photo>, List<PhotoViewModel>>(photos);
        }

        public void Insert(PhotoViewModel item)
        {
            Photo photo = mapper.Map<PhotoViewModel, Photo>(item);

            unitOfWork.Photoes.Insert(photo);
            unitOfWork.Save();
        }

        public void Update(PhotoViewModel item)
        {
            Photo photo = mapper.Map<PhotoViewModel, Photo>(item);

            unitOfWork.Photoes.Update(photo);
            unitOfWork.Save();
        }
    }
}
