using AutoMapper;
using BAL.Interfaces;
using Model.DTOs;
using Model.Interfaces;
using Model.ViewModels.PhoneViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Model.ViewModels.PhotoViewModels;
using WebCustomerApp.Models;
using BAL.Wrappers;
using System.Drawing.Imaging;

namespace BAL.Managers
{
    public class PhotoManager:BaseManager,IPhotoManager
    {
        private readonly IFileIoWrapper fileIo;
        public PhotoManager(IUnitOfWork unitOfWork, IMapper mapper, IFileIoWrapper fileIo) : base(unitOfWork, mapper)
        {
            this.fileIo = fileIo;
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

        public TransactionResultDTO AddImage(ImageViewModel item)
        {

            if (item.PhotoFile == null)
                return new TransactionResultDTO() { Success = false, Details = "No logo sent" };

            if (item.CommodityId == 0)
                return new TransactionResultDTO() { Success = false, Details = "Empty operator id" };

            // Create bitmap
            var stream = item.PhotoFile.OpenReadStream();
            Bitmap image;
            try
            {
                image = new Bitmap(stream);
                image = new Bitmap(image, 1000, 1000);
                // .setResolution() doesnt work. Bug, possibly
            }
            catch (ArgumentException)
            {
                return new TransactionResultDTO() { Success = false, Details = "Image data corrupted" };
            }
            catch (Exception)
            {
                return new TransactionResultDTO() { Success = false, Details = "Image can't be resized" };
            }

            if (!fileIo.Exists("wwwroot/images/OperatorLogo/"))
            {
                try
                {
                    fileIo.CreateDirectory("wwwroot/images/OperatorLogo/");
                }
                catch (Exception)
                {
                    return new TransactionResultDTO() { Success = false, Details = "Can't create directory for logos" };
                }
            }

            try
            {
                fileIo.SaveBitmap(image, "wwwroot/images/OperatorLogo/Logo_Id=" + Convert.ToString(item.CommodityId) + ".png"
                    , ImageFormat.Png);
            }
            catch (ArgumentNullException)
            {
                return new TransactionResultDTO() { Success = false, Details = "Internal error" };
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                return new TransactionResultDTO() { Success = false, Details = "Saving destination cannot be reached" };
            }
            catch (Exception)
            {
                return new TransactionResultDTO() { Success = false, Details = "Internal error" };
            }

            return new TransactionResultDTO() { Success = true };

            
            //Photo photo = mapper.Map<PhotoViewModel, Photo>(item);

            //unitOfWork.Photoes.Insert(photo);
            //unitOfWork.Save();
        }

        public void Update(PhotoViewModel item)
        {
            Photo photo = mapper.Map<PhotoViewModel, Photo>(item);

            unitOfWork.Photoes.Update(photo);
            unitOfWork.Save();
        }
    }
}
