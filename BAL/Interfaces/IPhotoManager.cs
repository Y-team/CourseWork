using System;
using System.Collections.Generic;
using System.Text;
using Model.ViewModels.PhoneViewModels;

namespace BAL.Interfaces
{
    public interface IPhotoManager
    {
        PhotoViewModel GetById(int id);
        IEnumerable<PhotoViewModel> GetAllPhotos();
        void Insert(PhotoViewModel item);
        void Update(PhotoViewModel item);
        void Delete(int id);
    }
}
