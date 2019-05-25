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
            CreateMap<Company, CompanyViewModel>().ForMember(dest => dest.RecipientViewModels, opt => opt.MapFrom(src => src.Recipients));
            CreateMap<CompanyViewModel, Company>().ForMember(dest => dest.Recipients, opt => opt.MapFrom(src => src.RecipientViewModels));
            CreateMap<Recipient, RecipientViewModel>().ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == 1 ? "Male" : "Female"))
                            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone.PhoneNumber));
            CreateMap<RecipientViewModel, Recipient>().ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == "Male" ? 1 : 0));

            CreateMap<Operator, OperatorViewModel>().ReverseMap();
            CreateMap<Code, CodeViewModel>().ReverseMap();

            CreateMap<StopWord, StopWordViewModel>();
            CreateMap<StopWordViewModel, StopWord>();
          
            CreateMap<Tariff, TariffViewModel>();
            CreateMap<TariffViewModel, Tariff>();

            CreateMap<Contact, ContactViewModel>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == 1 ? "Male" : "Female"));
            CreateMap<ContactViewModel, Contact>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender == "Male" ? 1 : 0));
            CreateMap<Code, CodeViewModel>();
            CreateMap<CodeViewModel, Code>();

            CreateMap<ApplicationGroup, GroupViewModel>().ForMember(dest => dest.ApplicationUsers, opt => opt.MapFrom(src => src.ApplicationUsers));
            CreateMap<GroupViewModel, ApplicationGroup>().ForMember(dest => dest.ApplicationUsers, opt => opt.MapFrom(src => src.ApplicationUsers));

            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<UserViewModel, ApplicationUser>();        
            //==================================
            CreateMap<Basket, BasketViewModel>()
                .ForMember(bview => bview.Id, bmod => bmod.MapFrom(src => src.Id))
                .ForMember(bview => bview.UserId, bmod => bmod.MapFrom(src => src.UserId))
                .ForMember(bview => bview.Description, bmod => bmod.MapFrom(src => src.Description))
                .ForMember(bview=>bview.UserName,bmod=>bmod.MapFrom(src=>src.ApplicationUser.UserName))
                .ReverseMap();

            CreateMap<BlockedUser, BlockedUserViewModel>().ReverseMap();

            CreateMap<Commodity, CommodityViewModel>().ReverseMap();

            CreateMap<LongDescription, LongDescriptionViewModel>().ReverseMap();

            CreateMap<Moderator, ModeratorViewModel>()
                .ForMember(mview => mview.Id, mmod => mmod.MapFrom(src => src.Id))
                .ForMember(mview => mview.NameCompany, mmod => mmod.MapFrom(src => src.NameCompany))
                .ForMember(mview => mview.UserId, mmod => mmod.MapFrom(src => src.UserId))
                .ForMember(mview => mview.UserName, mmod => mmod.MapFrom(src => src.ApplicationUser.UserName))
                .ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Photo, PhotoViewModel>().ReverseMap();

        }
    }
}
