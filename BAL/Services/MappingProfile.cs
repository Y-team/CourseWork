using AutoMapper;
using Model.ViewModels.TariffViewModels;
using Model.ViewModels.CompanyViewModels;
using Model.ViewModels.RecipientViewModels;
using Model.ViewModels.ContactViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Model.ViewModels.OperatorViewModels;
using Model.ViewModels.CodeViewModels;
using WebCustomerApp.Models;
using Model.ViewModels.StopWordViewModels;
using Model.ViewModels.GroupViewModels;
using Model.ViewModels.UserViewModels;
using BAL.Managers;
using Model.DTOs;
using Model.ViewModels.BasketViewModels;
using Model.ViewModels.BlokedUserBiewModels;
using Model.ViewModels.CommodityViewModels;
using Model.ViewModels.LongDescriptionViewModels;
using Model.ViewModels.ModeratorViewModels;
using Model.ViewModels.OrderViewModels;
using Model.ViewModels.PhoneViewModels;
using System.IO;
using Model.ViewModels.OrderCommodityViewModels;

namespace BAL.Services
{
    /// <summary>
    ///  Mapper class for each mapping that is performed, inherited from Automapper Profile class
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor with all mappings
        /// </summary>
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
              
            //==================================
            CreateMap<Basket, BasketViewModel>()
                .ForMember(bview => bview.Id, bmod => bmod.MapFrom(src => src.Id))
                .ForMember(bview => bview.UserId, bmod => bmod.MapFrom(src => src.UserId))
                .ForMember(bview => bview.Description, bmod => bmod.MapFrom(src => src.Description))
                .ForMember(bview=>bview.UserName,bmod=>bmod.MapFrom(src=>src.ApplicationUser.UserName))
                .ReverseMap();

            CreateMap<BlockedUser, BlockedUserViewModel>().ReverseMap();

            CreateMap<Commodity, CommodityViewModel>()
                .ForMember(ovm => ovm.PhotoName, opt => opt.MapFrom(com => GetPhoto(com)));
            CreateMap<CommodityViewModel, Commodity>();

            CreateMap<Commodity, CommodityUserViewModel>()
                .ForMember(ovm => ovm.PhotoName, opt => opt.MapFrom(com => GetPhoto(com)));
            CreateMap<CommodityUserViewModel, Commodity>();


            CreateMap<LongDescription, LongDescriptionViewModel>().ReverseMap();

            CreateMap<Moderator, ModeratorViewModel>()
                .ForMember(mview => mview.Id, mmod => mmod.MapFrom(src => src.Id))
                .ForMember(mview => mview.NameCompany, mmod => mmod.MapFrom(src => src.NameCompany))
                .ForMember(mview => mview.UserId, mmod => mmod.MapFrom(src => src.UserId))
                .ForMember(mview => mview.UserName, mmod => mmod.MapFrom(src => src.ApplicationUser.UserName))
                .ForMember(mview => mview.Email, mmod => mmod.MapFrom(a => a.ApplicationUser.Email));


            CreateMap<ModeratorViewModel, Moderator>()
                .ForMember(mview => mview.Id, mmod => mmod.MapFrom(src => src.Id))
                .ForMember(mview => mview.NameCompany, mmod => mmod.MapFrom(src => src.NameCompany))
                .ForMember(mview => mview.UserId, mmod => mmod.MapFrom(src => src.UserId));
               


            CreateMap<OrderUser, OrderUserViewModel>().ReverseMap();
            CreateMap<Photo, PhotoViewModel>().ReverseMap();
            CreateMap<OrderCommodities, OrderCommodityViewModel>()
                .ForMember(ocview => ocview.CommodityId, oc => oc.MapFrom(src => src.CommodityId))
                .ForMember(ocview => ocview.CommodityName, oc => oc.MapFrom(src => src.Commodity.Name))
                .ForMember(ocview => ocview.IsConfirmeds, oc => oc.MapFrom(src => src.IsConfirmeds));

            CreateMap<OrderCommodityViewModel, OrderCommodities>();
               
        }

        private string GetPhoto(Commodity commodity)
        {
            string filePath = "wwwroot/images/CommodityPhotos/Photo_Id=" + Convert.ToString(commodity.Id) + ".png";
            if (File.Exists(filePath))
            {
                return "/images/CommodityPhotos/Photo_Id=" + Convert.ToString(commodity.Id) + ".png";
            }
            else
              //  return "/images/CommodityPhotos/Photo_Id=" + Convert.ToString(commodity.Id) + ".png";
            return null;
        }
    }
}
