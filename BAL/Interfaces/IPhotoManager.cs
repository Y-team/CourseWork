using System;
using System.Collections.Generic;
using System.Text;
using Model.DTOs;
using Model.ViewModels.PhoneViewModels;
using Model.ViewModels.PhotoViewModels;

namespace BAL.Interfaces
{
    public interface IPhotoManager
    {
        PhotoViewModel GetById(int id);
        IEnumerable<PhotoViewModel> GetAllPhotos();
        // void Insert(PhotoViewModel item);
        TransactionResultDTO AddImage(ImageViewModel item);
        void Update(PhotoViewModel item);
        void Delete(int id);
    }
}
